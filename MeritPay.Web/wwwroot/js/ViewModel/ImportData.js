function initVm(arg) {
    var vm = kendo.observable({              

        saveDS: new kendo.data.DataSource({
            transport: {
                read: {
                    dataType: "json",
                    url: arg.saveFileUrl,
                    type: 'POST',
                    data: {}
                }
            },            
            schema: {
                parse: function (data) {
                    blockUI(false);
                    showAlert(data.Message, null, "پیغام");                  
                    vm.gridDataDS.read();
                    return data;
                }
            }
        }),

        onSave: function () {
            blockUI(true);
            vm.saveDS.read();
        },       

        gridDataDS: new kendo.data.DataSource({
            transport: {
                read: {
                    dataType: "json",
                    url: arg.gridDataUrl,
                    type: 'POST',
                    data: {},
                }
            },
            schema: {
                parse: function (result) {
                    blockUI(false);
                    return result;
                },
                total: "total",
                data: "data",
            },
            pageSize: 10,
            serverPaging: true
        }),
   

    });
    return vm;
}