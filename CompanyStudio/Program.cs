using MesaSuite.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using MesaSuite.Common.Utility;
using System.Collections.Generic;
using CompanyStudio.Models;
using System.Collections.Specialized;

namespace CompanyStudio
{
    public static class Program
    {
        private static RunByURIEngine runByURIEngine;

        public static void Main(string[] args)
        {
            Application.Run(new SecuredApplicationContext(() => new frmStudio(), "company", "Company Studio", Main, args));
        }

        public static RunByURIEngine GetOrCreateRunByURIEngine()
        {
            if (runByURIEngine == null)
            {
                runByURIEngine = new RunByURIEngine(OpenFormByURI, TryStudioFormInvoke);
            }

            return runByURIEngine;
        }

        private static object TryStudioFormInvoke(Delegate method)
        {
            frmStudio studio = Application.OpenForms.OfType<frmStudio>().FirstOrDefault();
            if (studio != null)
            {
                return studio.Invoke(method);
            }

            return null;
        }

        private static async void OpenFormByURI(Form form, NameValueCollection additionalData)
        {
            if (form is frmStudio)
            {
                Application.OpenForms.OfType<frmStudio>().FirstOrDefault();
                form.BringToFront();
                return;
            }

            if (!(form is BaseCompanyStudioContent studioContent) || !additionalData.AllKeys.Contains("CompanyID", StringComparer.OrdinalIgnoreCase) || !long.TryParse(additionalData["CompanyID"], out long companyID))
            {
                return;
            }

            frmStudio currentStudio = null;
            int attempts = 0;
            while(currentStudio == null && attempts < 10)
            {
                currentStudio = Application.OpenForms.OfType<frmStudio>().FirstOrDefault();
                if (currentStudio != null)
                {
                    break;
                }

                attempts++;
                await Task.Delay(500);// 500ms * 10 attempts = 5 seconds
            }

            if (currentStudio == null) // Guess it's not opening /shrug
            {
                return;
            }

            while (currentStudio.IsLoading)
            {
                await Task.Delay(50);
            }

            currentStudio.DecorateStudioContent(studioContent);
            Company company = currentStudio.Companies.FirstOrDefault(c => c.CompanyID == companyID);
            if (company == null)
            {
                return;// User not a part of this company
            }
            studioContent.Company = company;

            if (studioContent is ILocationScoped locationScoped)
            {
                if (!additionalData.AllKeys.Contains("LocationID", StringComparer.OrdinalIgnoreCase) || !long.TryParse(additionalData["LocationID"], out long locationID) || !company.Locations.Any(l => l.LocationID == locationID))
                {
                    return;
                }

                locationScoped.LocationModel = company.Locations.FirstOrDefault(l => l.LocationID == locationID);
                if (locationScoped.LocationModel == null)
                {
                    return; // User not a part of this location
                }
            }

            // check to see if form is already open
            BaseCompanyStudioContent existingStudioContent = Application.OpenForms.OfType<BaseCompanyStudioContent>().Where(f =>
                                                                f.GetType() == form.GetType() &&
                                                                f.Company.CompanyID == studioContent.Company.CompanyID &&
                                                                (f is ILocationScoped fLocationScoped ? fLocationScoped.LocationModel.LocationID == (studioContent as ILocationScoped).LocationModel.LocationID : true))
                                                                .FirstOrDefault();

            Action crossThreadAction = null;
            if (existingStudioContent != null)
            {
                crossThreadAction = () =>
                {
                    existingStudioContent.BringToFront();
                    currentStudio.TopMost = true;
                    currentStudio.TopMost = false;
                };
            }
            else
            {
                crossThreadAction = () =>
                {
                    currentStudio.ActiveCompany = studioContent.Company;
                    if (currentStudio is ILocationScoped locationScopedForSettingStudio)
                    {
                        currentStudio.ActiveLocation = locationScopedForSettingStudio.LocationModel;
                    }
                    studioContent.Show(currentStudio.dockPanel);

                    currentStudio.TopMost = true;
                    currentStudio.TopMost = false;
                };
            }

            currentStudio.Invoke(new MethodInvoker(crossThreadAction));
        }
    }
}
