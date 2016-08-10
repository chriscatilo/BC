'use strict';
(function () {
    angular.module("eqcs.core.service")
        .factory("confirmationService", [
            function () {
                var confirmation = {
                    delete: function (title, doCommand) {

                        var kendoWindow = $("<div />").kendoWindow({
                            title: title,
                            resizable: false,
                            modal: true,
                            width: 400,
                            height: 150,
                        });

                        kendoWindow.data("kendoWindow")
                            .content($("#delete-confirmation").html())
                            .center().open();

                        kendoWindow
                            .find(".delete-confirm,.delete-cancel")
                            .click(function () {
                                if ($(this).hasClass("delete-confirm")) {
                                    doCommand();
                                }

                                kendoWindow.data("kendoWindow").close();
                            })
                            .end();

                        setTimeout(function () {
                            $('.delete-cancel').focus();
                        }, 400);
                    }
                };

                return confirmation;
            }
        ]);
})();