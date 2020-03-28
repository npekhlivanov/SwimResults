// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

(function () {
    $("[data-toggle='tooltip']").tooltip();
    $("[data-toggle='popover']").popover({ // https://getbootstrap.com/docs/4.0/components/popovers/#example-using-the-container-option
        container: 'body'
    });
})();
