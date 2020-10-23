function initVm(arg) {
    var vm = kendo.observable({

        personCode: '',
        branchCode: '',
        firstName: '',
        branchName: '',
        lastName: '',
        zoneName: '',
        studyJob: '',
        birthDate: '',
        grade: '',
        employeeDate: '',
        studyBranch: '',

        taScore: '',
        taRankInBranch: '',
        taRankInZone: '',
        taRankInBank: '',

        GroupScore: '',
        GroupRankInBranch: '',
        GroupRankInZone: '',
        GroupRankInBank: '',

        ArzeshScore: '',
        ArzeshRankInBranch: '',
        ArzeshRankInZone: '',
        ArzeshRankInBank: '',

        saveDS: new kendo.data.DataSource({
            transport: {
                read: {
                    dataType: "json",
                    url: arg.getDataReportUrl,
                    type: 'POST',
                    data: {}
                }
            },
            schema: {
                parse: function (data) {
                    blockUI(false);
                    if (data.Success !== true) {
                        showAlert(data.Message, null, "پیغام");
                    }

                    vm.set('personCode', data.Data.PersonCode);
                    vm.set('branchCode', data.Data.branchCode);
                    vm.set('firstName', data.Data.FirstName);
                    vm.set('branchName', data.Data.branchName);
                    vm.set('lastName', data.Data.LastName);
                    vm.set('zoneName', data.Data.zoneName);
                    vm.set('studyJob', data.Data.StudyJob);
                    vm.set('birthDate', data.Data.BirthDate);
                    vm.set('grade', data.Data.Grade);
                    vm.set('employeeDate', data.Data.EmployeeDate);
                    vm.set('studyBranch', data.Data.StudyBranch);

                    vm.set('taScore', data.Data.TaScore);
                    vm.set('taRankInBranch', data.Data.TaRankInBranch);
                    vm.set('taRankInZone', data.Data.TaRankInZone);
                    vm.set('taRankInBank', data.Data.TaRankInBank);

                    vm.set('GroupScore', data.Data.GroupScore);
                    vm.set('GroupRankInBranch', data.Data.GroupRankInBranch);
                    vm.set('GroupRankInZone', data.Data.GroupRankInZone);
                    vm.set('GroupRankInBank', data.Data.GroupRankInBank);

                    vm.set('ArzeshScore', data.Data.ArzeshScore);
                    vm.set('ArzeshRankInBranch', data.Data.ArzeshRankInBranch);
                    vm.set('ArzeshRankInZone', data.Data.ArzeshRankInZone);
                    vm.set('ArzeshRankInBank', data.Data.ArzeshRankInBank);
                    return data;
                }
            }
        }),

        onLoad: function () {
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