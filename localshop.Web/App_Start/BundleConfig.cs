using System.Web;
using System.Web.Optimization;

namespace localshop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            RegisterAdminStyles(bundles);
            RegisterAdminScripts(bundles);

            RegisterClientStyles(bundles);
            RegisterClientScripts(bundles);

            bundles.Add(new ScriptBundle("~/content/modernizr").Include("~/Assets/plugins/modernizr-2.8.3.js"));
            bundles.Add(new ScriptBundle("~/content/removeAds").Include("~/Assets/plugins/someeAdsRemover.js"));
        }

        private static void RegisterAdminStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/admin/css/vendor").Include(
                "~/Assets/admin/css/bootstrap.css",
                "~/Assets/admin/css/app.css"));

            bundles.Add(new StyleBundle("~/content/admin/css/toastr").Include("~/Assets/admin/libs/toastr/toastr.min.css"));

            bundles.Add(new StyleBundle("~/content/admin/css/datatables_sweetalert").Include(
                "~/Assets/admin/libs/datatables/dataTables.bootstrap4.css",
                "~/Assets/admin/libs/datatables/responsive.bootstrap4.css",
                "~/Assets/admin/libs/sweetalert2/sweetalert2.min.css"));

            bundles.Add(new StyleBundle("~/content/admin/css/dropify").Include("~/Assets/admin/libs/dropify/dropify.min.css"));
        }

        private static void RegisterAdminScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/content/admin/js/vendor").Include("~/Assets/admin/js/vendor.js"));

            bundles.Add(new ScriptBundle("~/content/admin/js/app").Include("~/Assets/admin/js/app.js"));

            bundles.Add(new ScriptBundle("~/content/admin/js/toastr").Include(
                "~/Assets/admin/libs/toastr/toastr.min.js",
                "~/Assets/admin/js/pages/toastr.init.js"));

            bundles.Add(new ScriptBundle("~/content/admin/js/datatables_sweetalert").Include(
                "~/Assets/admin/libs/datatables/jquery.dataTables.min.js",
                "~/Assets/admin/libs/datatables/dataTables.bootstrap4.js",
                "~/Assets/admin/libs/datatables/dataTables.responsive.min.js",
                "~/Assets/admin/libs/datatables/responsive.bootstrap4.min.js",
                "~/Assets/admin/libs/sweetalert2/sweetalert2.min.js",
                "~/Assets/admin/js/pages/sweet-alerts.init.js"));

            bundles.Add(new ScriptBundle("~/content/admin/js/validation").Include(
                "~/Assets/admin/libs/parsleyjs/parsley.min.js",
                "~/Assets/admin/js/pages/form-validation.init.js"));

            bundles.Add(new ScriptBundle("~/content/admin/js/dropify").Include(
                "~/Assets/admin/libs/dropify/dropify.min.js",
                "~/Assets/admin/js/pages/form-fileupload.init.js"));

            // Controller
            bundles.Add(new ScriptBundle("~/content/admin/js/category/index").Include("~/Assets/admin/js/controllers/category/index.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/city/index").Include("~/Assets/admin/js/controllers/city/index.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/configuration/index").Include("~/Assets/admin/js/controllers/configuration/index.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/contact/index").Include("~/Assets/admin/js/controllers/contact/index.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/homePage/addBanner").Include("~/Assets/admin/js/controllers/homePage/addBanner.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/homePage/banners").Include("~/Assets/admin/js/controllers/homePage/banners.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/homePage/editBanner").Include("~/Assets/admin/js/controllers/homePage/editBanner.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/homePage/sepcialFeatured").Include("~/Assets/admin/js/controllers/homePage/sepcialFeatured.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/order/index").Include("~/Assets/admin/js/controllers/order/index.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/product/add").Include("~/Assets/admin/js/controllers/product/add.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/product/edit").Include("~/Assets/admin/js/controllers/product/edit.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/product/index").Include("~/Assets/admin/js/controllers/product/index.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/review/index").Include("~/Assets/admin/js/controllers/review/index.js"));
            bundles.Add(new ScriptBundle("~/content/admin/js/user/index").Include("~/Assets/admin/js/controllers/user/index.js"));
        }

        public static void RegisterClientStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/client/css/vendor").Include("~/Assets/client/css/vendor/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/content/client/css/app").Include("~/Assets/client/css/style.css"));

            bundles.Add(new StyleBundle("~/content/client/css/vendor/plugins").Include(
               "~/Assets/client/css/plugins/animate.css",
               "~/Assets/client/css/plugins/owl-carousel.css",
               "~/Assets/client/css/plugins/slick.css",
               "~/Assets/client/css/plugins/magnific-popup.css",
               "~/Assets/client/css/plugins/jquery-ui.css",
               "~/Assets/client/css/plugins/toastr.min.css"));

            bundles.Add(new StyleBundle("~/content/client/css/content-style").Include(
               "~/Assets/plugins/ckeditor5-build-classic-16.0.0/content-styles/content-styles.css"));

            bundles.Add(new StyleBundle("~/content/client/css/tracking").Include(
                "~/Assets/client/css/plugins/animate.css",
                "~/Assets/client/css/plugins/jquery-ui.css",
                "~/Assets/client/css/plugins/toastr.min.css",
                "~/Assets/client/css/plugins/print.min.css"));
        }

        public static void RegisterClientScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/content/client/js/vendor").Include(
                "~/Assets/client/js/vendor/modernizr-3.6.0.min.js",
                "~/Assets/client/js/vendor/jquery-3.3.1.min.js",
                "~/Assets/client/js/vendor/popper.js",
                "~/Assets/client/js/vendor/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/content/client/js/vendor/plugins").Include(
                "~/Assets/client/js/plugins/countdown.js",
                "~/Assets/client/js/plugins/images-loaded.js",
                "~/Assets/client/js/plugins/isotope.js",
                "~/Assets/client/js/plugins/instafeed.js",
                "~/Assets/client/js/plugins/jquery-ui.js",
                "~/Assets/client/js/plugins/jquery-ui-touch-punch.js",
                "~/Assets/client/js/plugins/magnific-popup.js",
                "~/Assets/client/js/plugins/owl-carousel.js",
                "~/Assets/client/js/plugins/scrollup.js",
                "~/Assets/client/js/plugins/waypoints.js",
                "~/Assets/client/js/plugins/slick.js",
                "~/Assets/client/js/plugins/wow.js",
                "~/Assets/client/js/plugins/textillate.js",
                "~/Assets/client/js/plugins/elevatezoom.js",
                "~/Assets/client/js/plugins/sticky-sidebar.js",
                "~/Assets/client/js/plugins/smoothscroll.js",
                "~/Assets/client/js/plugins/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/content/client/js/validation").Include("~/Assets/client/js/plugins/parsley.js"));

            bundles.Add(new ScriptBundle("~/content/client/js/app").Include("~/Assets/client/js/main.js"));

            bundles.Add(new ScriptBundle("~/content/client/js/tracking").Include(
                "~/Assets/client/js/plugins/smoothscroll.js",
                "~/Assets/client/js/plugins/toastr.min.js",
                "~/Assets/client/js/plugins/print.min.js"));

            // Controller
            bundles.Add(new ScriptBundle("~/content/client/js/account/changePassword").Include("~/Assets/client/js/controllers/account/changePassword.js"));
            bundles.Add(new ScriptBundle("~/content/client/js/account/forgotPassword").Include("~/Assets/client/js/controllers/account/forgotPassword.js"));
            bundles.Add(new ScriptBundle("~/content/client/js/account/info").Include("~/Assets/client/js/controllers/account/info.js"));
            bundles.Add(new ScriptBundle("~/content/client/js/account/loginRegister").Include("~/Assets/client/js/controllers/account/loginRegister.js"));
            bundles.Add(new ScriptBundle("~/content/client/js/account/resetPassword").Include("~/Assets/client/js/controllers/account/resetPassword.js"));
            bundles.Add(new ScriptBundle("~/content/client/js/cart/index").Include("~/Assets/client/js/controllers/cart/index.js"));
            bundles.Add(new ScriptBundle("~/content/client/js/contact/index").Include("~/Assets/client/js/controllers/contact/index.js"));
            bundles.Add(new ScriptBundle("~/content/client/js/checkout/index").Include("~/Assets/client/js/controllers/checkout/index.js"));
            bundles.Add(new ScriptBundle("~/content/client/js/product/detail").Include("~/Assets/client/js/controllers/product/detail.js"));
            bundles.Add(new ScriptBundle("~/content/client/js/shop/index").Include("~/Assets/client/js/controllers/shop/index.js"));
        }
    }
}
