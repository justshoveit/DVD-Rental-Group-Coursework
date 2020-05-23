var AttendanceVM = function () {
    var self = this;

    var models = {
        master: function (item) {
            item = item || {};
            this.CourseId = ko.observable(item.CourseId || "");
            this.SemesterId = ko.observable(item.SemesterId || "");
            this.TimetableId = ko.observable(item.TimetableId || "");
            this.ModuleId = ko.observable(item.ModuleId || 0);
            this.AttendanceDate = ko.observable(item.AttendanceDate || new Date());
            this.Details = ko.observableArray(item.Details || []);
            this.Day = ko.observable(item.Day || 1);
        },
        detail: function (item) {
            item = item || {};
            this.Id = ko.observable(item.Id || 0);
            this.StudentId = ko.observable(item.StudentId || 0);
            this.StudentRollNo = ko.observable(item.StudentRollNo || "");
            this.Name = ko.observable(item.Name || "");
            this.Status = ko.observable(item.Status || "");
            this.Remarks = ko.observable(item.Remarks || "");
        }
    }

    var UiElements = {
        initialize: function () {
            //self.enableCourse = ko.observable(false);
            self.enableSemester = ko.observable(false);
            self.enableTimetable = ko.observable(false);
            self.ErrorMessage = ko.observable('');
            self.CourseList = [
                { Text: 'BBA', Value: '1' },
                { Text: 'BIT', Value: '2' },
            ];
            self.SemesterList = [
                { Text: 'Semester 1', Value: '1' },
                { Text: 'Semester 2', Value: '2' },
            ];

            if (modelObj) {
                self.CourseList = ko.observableArray(modelObj.CourseList || self.CourseList);
                self.SemesterList = ko.observableArray(modelObj.SemesterList || self.SemesterList);
            }
            self.TimetableList = ko.observableArray([]);

            self.CourseList.unshift({ Text: 'Select Option', Value: '' })
            self.SemesterList.unshift({ Text: 'Select Option', Value: '' })
            self.TimetableList.unshift({ Text: 'Select Option', Value: '' })
        }
    }

    var UiEvents = {
        init: function () {
            self.master = ko.observable(new models.master());
            UiElements.initialize();
            UiEvents.change();
            //UiEvents.getInitialData();
        }
        ,change: function () {
            self.courseChangeEvent = function () {
                self.enableSemester(!!self.master().CourseId());
            };

            self.semesterChangeEvent = function () {
                $.ajax({
                    method: "GET",
                    url: '/Attendance/GetTimetableList',
                    data: {
                        courseId: self.master().CourseId(),
                        semesterId: self.master().SemesterId()
                    },
                    success: function (data) {
                        console.log(data);
                        if (data) {
                            self.enableTimetable(true);
                            self.TimetableList(data); //TODO DATA
                            self.TimetableList.unshift({ Text: 'Select Option', Value: '' })
                        } else {
                            Swal.fire("Warning", "Timetable for specified data doesn't exist", 'warning');
                        }
                    },
                    error: function (data) {
                        Swal.fire("Error","Invalid network request", 'error');
                    }
                });
            };

            self.timetableChangeEvent = function () {
                if (!self.master().TimetableId()) {
                    return;
                }
                $.ajax({
                    method: "GET",
                    url: '/Attendance/GetStudentList',
                    data: {
                        courseId: self.master().CourseId(),
                        semesterId: self.master().SemesterId(),
                        timetableId: self.master().TimetableId()
                    },
                    success: function (data) {
                        console.log(data);
                        if (data) {
                            self.master().Details(ko.utils.arrayMap(data, function (item) {
                                item.StudentId = item.Id;
                                item.Id = 0;
                                return new models.detail(item)
                            }));
                            console.log(ko.toJS(self.master().Details()));
                        }
                    },
                    error: function (data) {
                        console.log(data);
                    }
                });
            };

            self.SaveEvent = function () {
                var model = ko.toJS(self.master);
                console.log(model);
                $.ajax({
                    method: "POST",
                    dataType: "json",
                    contentType: "application/json",
                    url: '/Attendance/SaveAttendance',
                    data: JSON.stringify({ data: model }),
                    success: function (data) {
                        console.log(data);
                        if (data.Success) {
                            console.log(data);
                            window.open("/Attendance/Index", "_self");
                            //swal("Success", "Attendance successfully saved", 'success');
                            //setTimeout(function () {
                            //    window.open("/Attendance/Index", "self");
                            //}, 500)
                        } else {
                            self.ErrorMessage(data.Error);
                        }
                    },
                    error: function (data) {
                        console.log(data);
                        Swal.fire("Error", "Invalid network request", 'error');
                    }
                });
            };

        }
        , getInitialData: function () {

        }
    }
    UiEvents.init();
};