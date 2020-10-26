function initVm(arg) {
    var vm = kendo.observable({

        personCode: '',
        firstName: '',
        lastName: '',
        studyJob: '',
        birthDate: '',
        grade: '',
        employeeDate: '',
        studyBranch: '',
        branchCode        : '',
        branchName       : '',
        zoneName          : '',
        arzyab1          : '',
        arzyab2          : '',
        maghtaArzeshyabi: '',

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

        htmlTest: '',

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
                    vm.set('firstName', data.Data.FirstName);
                    vm.set('lastName', data.Data.LastName);
                    vm.set('studyJob', data.Data.StudyJob);
                    vm.set('birthDate', data.Data.BirthDate);
                    vm.set('grade', data.Data.Grade);
                    vm.set('employeeDate', data.Data.EmployeeDate);
                    vm.set('studyBranch', data.Data.StudyBranch);
                    vm.set('branchCode', data.Data.BranchCode);
                    vm.set('branchName', data.Data.BranchName);
                    vm.set('zoneName', data.Data.ZoneName);
                    vm.set('arzyab1', data.Data.Arzyab1);
                    vm.set('arzyab2', data.Data.Arzyab2);
                    vm.set('maghtaArzeshyabi', data.Data.MaghtaArzeshyabi);

                    if (data.Data.ReportTaDataList !== null) {
                        var html = "<table style='border-width: 0px;border-spacing:0px;width:100%'>";
                        var scoreIndex = "";
                        for (var i = 0; i < data.Data.ReportTaDataList.length; i++) {
                            var c = 0;
                            if (i > 0) {
                                scoreIndex = data.Data.ReportTaDataList[i - 1].ScoreIndexTitle;
                            }

                            var row = data.Data.ReportTaDataList[i];                       
                            if (scoreIndex !== row.ScoreIndexTitle) {  
                                for (var j = 0; j < data.Data.ReportTaDataList.length; j++) {
                                    if (row.ScoreIndexTitle === data.Data.ReportTaDataList[j].ScoreIndexTitle) {
                                        c++;
                                    }
                                }
                                html += "<tr>" +
                                    "<td rowspan='" + c + "'  style='width:20%;background-color:aliceblue'>" + row.ScoreIndexTitle + "</td>" +
                                    "<td style='width:20%;background-color:aliceblue'>" + row.ScoreSubIndexTitle + "</td>" +
                                    "<td style='width:20%'>" + row.Value + "</td>" +
                                    "<td style='width:10%'>" + row.Score + "</td>" +
                                    "<td style='width:10%'>" + row.RankInBranch + "</td>" +
                                    "<td style='width:10%'>" + row.RankInZone + "</td>" +
                                    "<td style='width:10%'>" + row.RankInBank + "</td>" +
                                    "</tr>";
                            } else {
                                html += "<tr>" +
                                    "<td style='width:20%;background-color:aliceblue'>" + row.ScoreSubIndexTitle + "</td>" +
                                    "<td style='width:20%'>" + row.Value + "</td>" +
                                    "<td style='width:10%'>" + row.Score + "</td>" +
                                    "<td style='width:10%'>" + row.RankInBranch + "</td>" +
                                    "<td style='width:10%'>" + row.RankInZone + "</td>" +
                                    "<td style='width:10%'>" + row.RankInBank + "</td>" +
                                    "</tr>";
                            }
                        }
                        html += "</table>"
                        vm.set('htmlTest', html);
                    }

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