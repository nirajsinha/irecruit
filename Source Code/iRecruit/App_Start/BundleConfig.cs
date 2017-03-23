using System.Web.Optimization;

namespace iRecruit
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/Scripts/assets/jquery/jquery-2.1.1.min.js",
                        "~/Scripts/assets/jquery/jquery-ui.min.js"
            ));

            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                        "~/Scripts/assets/bootstrap/bootstrap.min.js"
            ));

            bundles.Add(new ScriptBundle("~/scripts/angular").Include(
                        "~/Scripts/assets/angular/angular.min.js",
                        "~/Scripts/assets/angular-sanitize/angular-sanitize.min.js",
                        "~/Scripts/assets/angular-translate/angular-translate.min.js",
                        "~/Scripts/assets/angular-messages/angular-messages.min.js",
                        "~/Scripts/assets/angular-resource/angular-resource.min.js",
                        "~/scripts/assets/angular-cookies/angular-cookies.min.js",
                        "~/Scripts/assets/angular-routes/angular-routes/angular-route.js",
                        "~/Scripts/assets/angular-ui-bootstrap/ui-bootstrap-tpls-0.12.0.min.js",
                        "~/Scripts/assets/dialogs/dialogs-default-translations.js",
                        "~/Scripts/assets/dialogs/dialogs.min.js",
                        "~/Scripts/assets/angular-ui-router/angular-ui-router.js",
                        "~/Scripts/assets/angular-local-storage/angular-local-storage.js",
                        "~/Scripts/assets/lodash/lodash.js",
                        "~/scripts/assets/angular-wizard/angular-wizard.js",
                        "~/scripts/assets/d3.v3/d3.v3.min.js",
                        "~/scripts/assets/angular-charts/angular-charts.min.js",
                        "~/Scripts/assets/underscore/underscore-min.js"

            ));
                                    
            bundles.Add(new ScriptBundle("~/scripts/plugins").Include(
                        "~/Scripts/assets/moment/moment.min.js",
                        "~/Scripts/assets/moment/moment-timezone.min.js",
                        "~/Scripts/assets/plugins/jquery.scrollTo.min.js",
                        "~/Scripts/assets/plugins/toastr.min.js",
                        "~/Scripts/assets/plugins/chosen.jquery.js",
                        "~/Scripts/assets/plugins/jquery.xml2json.js",
                        "~/scripts/assets/plugins/jquery.form.min.js",
                        "~/scripts/assets/plugins/bootstrap3-wysihtml5.all.min.js",
                        "~/scripts/assets/plugins/jquery.easing.1.3.js",
                        "~/scripts/assets/plugins/jstz-1.0.4.min.js",
                        "~/scripts/assets/plugins/loading-bar.min.js"
                        
            ));
            
            bundles.Add(new ScriptBundle("~/scripts/app/services").Include(
                        "~/scripts/app/services/services.module.js",        
                        "~/scripts/app/services/persist.service.js",
                        "~/scripts/app/services/common.service.js"
            ));
            bundles.Add(new ScriptBundle("~/scripts/app/directives").Include(
                        "~/scripts/app/directives/directives.module.js",            
                        "~/scripts/app/directives/chosen.js",
                        "~/scripts/app/directives/repeat-completed.js",
                        "~/scripts/app/directives/appRoleMapping.js",
                        "~/scripts/app/directives/file-upload.js",
                        "~/scripts/app/directives/custom-validators.js", 
                        "~/scripts/app/directives/wysihtml5-editor.js",
                        "~/scripts/app/directives/navigation.js",
                        "~/scripts/app/directives/infinite-scroll.js",
                        "~/scripts/app/directives/datetimepicker.js",
                        "~/scripts/app/directives/module-name.js"
                        
            ));
            bundles.Add(new ScriptBundle("~/scripts/app/modules").Include(
                        "~/scripts/app/modules/home/home.module.js",
                        "~/scripts/app/modules/home/home.service.js",
                        "~/scripts/app/modules/home/home.ctrl.js",
                        "~/scripts/app/modules/indent/indent.module.js",
                        "~/scripts/app/modules/indent/indent.service.js",
                        "~/scripts/app/modules/indent/indent.ctrl.js",
                        "~/scripts/app/modules/indent/indent.tracker.ctrl.js",
                        "~/scripts/app/modules/indent/indent.timeline.ctrl.js",
                        "~/scripts/app/modules/resources/resources.module.js",
                        "~/scripts/app/modules/resources/profile.service.js",
                        "~/scripts/app/modules/resources/profile.edit.ctrl.js", 
                        "~/scripts/app/modules/resources/profile.view.ctrl.js",
                        "~/scripts/app/modules/resources/resume.search.ctrl.js",
                        "~/scripts/app/modules/resources/interview.feedback.ctrl.js",
                        "~/scripts/app/modules/resources/interview.schedule.ctrl.js",
                        "~/scripts/app/modules/settings/settings.module.js",
                        "~/scripts/app/modules/settings/settings.service.js",
                        "~/scripts/app/modules/settings/department.ctrl.js",
                        "~/scripts/app/modules/settings/skills.ctrl.js",
                        "~/scripts/app/modules/settings/user.ctrl.js"
            ));
            
            bundles.Add(new ScriptBundle("~/scripts/app").Include(
                        "~/scripts/app/global.js",
                        "~/scripts/app/app.js",
                        "~/scripts/app/routes.js"
            ));
            
            
  
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/css/jquery/themes/base/css").Include(
                        "~/css/themes/base/jquery-ui.css",
                        "~/css/themes/base/jquery-ui.structure.css",
                        "~/css/themes/base/jquery-ui.theme.css"
            ));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                        "~/css/bootstrap.css",
                        "~/css/bootstrap-responsive.css",
                        "~/css/bootstrap-theme.css",
                        "~/css/font-awesome.css"
                        ));
                
            bundles.Add(new StyleBundle("~/css/plugins").Include(
                        "~/css/ionicons.css",
                        "~/css/toastr.css",
                        "~/css/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                        "~/css/chosen.min.css",
                        "~/css/angular-wizard.css",
                        "~/css/loading-bar.min.css"
                        //"~/css/morris/morris.css",
                        //"~/css/jvectormap/jquery-jvectormap-1.2.2.css",
                        //"~/css/fullcalendar/fullcalendar.css",
                        //"~/css/daterangepicker/daterangepicker-bs3.css",
                        //"~/css/colorbox.css",
            ));

            bundles.Add(new StyleBundle("~/css/site").Include("~/css/site.css"));
            
            //bundles.Add(new StyleBundle("~/css/jquery/themes/base/css").Include(
            //            "~/css/themes/base/jquery.ui.core.css",
            //            "~/css/themes/base/jquery.ui.resizable.css",
            //            "~/css/themes/base/jquery.ui.selectable.css",
            //            "~/css/themes/base/jquery.ui.accordion.css",
            //            "~/css/themes/base/jquery.ui.autocomplete.css",
            //            "~/css/themes/base/jquery.ui.button.css",
            //            "~/css/themes/base/jquery.ui.dialog.css",
            //            "~/css/themes/base/jquery.ui.slider.css",
            //            "~/css/themes/base/jquery.ui.tabs.css",
            //            "~/css/themes/base/jquery.ui.datepicker.css",
            //            "~/css/themes/base/jquery.ui.progressbar.css",
            //            "~/css/themes/base/jquery.ui.theme.css"));

            //#if(DEBUG)
                BundleTable.EnableOptimizations = false;
            //#endif
            //#if(!DEBUG)
            //    BundleTable.EnableOptimizations = true;
            //#endif
        }
    }
}