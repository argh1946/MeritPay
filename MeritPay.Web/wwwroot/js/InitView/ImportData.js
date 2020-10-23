function initView(vm) {
   

    $("#requestGrid").kendoGrid({
        scrollable: true,
        selectable: true,
        autoBind: true,
        pageable: {
            input: true,
            messages: {
                display: "نمایش {0}-{1} از {2}",
                empty: "اطلاعاتی موجود نیست"
            }
        },
        columns: [
            //{ field: "FileName", title: "نام فایل" },
            { field: "UserName", title: "نام کاربر" },
            { field: "Age", title: "کد شعبه" },
            //{ command: [{ name: "btnDetaile", className: "btn-edit ", text: "دریافت فایل", click: vm.onExportPDF }], title: "اقدامات", width: 85 },

        ],
        dataSource: vm.gridDataDS,
    });
}

