var blogController = function () {

    this.initialize = function () {
        loadData();
        registerEvents();
        registerControls();
    }

    function registerEvents() {
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtNameM: { required: true },
                txtContentM: { required: true }
            }
        });

        $('#btnCreate').off('click').on('click', function () {
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');
        });
        $('#btnSearch').on('click', function () {
            loadData();
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "GET",
                url: "/Admin/Blog/GetById",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    khoaluan.startLoading();
                },
                success: function (response) {
                    var data = response;
                    $('#hidIdM').val(data.Id);
                    $('#txtNameM').val(data.Name);
                    $('#txtDescM').val(data.Description);
                    $('#txtImageM').val(data.Image);
                    $('#txtMetaDescriptionM').val(data.SeoDescription);
                    $('#txtSeoAliasM').val(data.SeoAlias);
                    CKEDITOR.instances.txtContent.setData(data.Content);
                    $('#txtContentM').val(data.Content);
                    $('#ckStatusM').prop('checked', data.Status == 1);
                    $('#ckHotM').prop('checked', data.HotFlag);
                    $('#ckShowHomeM').prop('checked', data.HomeFlag);
                    $('#modal-add-edit').modal('show');
                    khoaluan.stopLoading();

                },
                error: function (status) {
                    khoaluan.notify('Có lỗi xảy ra', 'error');
                    khoaluan.stopLoading();
                }
            });
        });
        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
             deleteBlog(that);
        });
        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });
        $("#fileInputImage").on('change', function () {
            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            $.ajax({
                type: "POST",
                url: "/Admin/Upload/UploadImage",
                contentType: false,
                processData: false,
                data: data,
                success: function (path) {
                    $('#txtImage').val(path);
                    khoaluan.notify('Upload image succesful!', 'success');

                },
                error: function () {
                    khoaluan.notify('There was error uploading files!', 'error');
                }
            });
        });
        $('#btnSave').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidIdM').val();
                var name = $('#txtNameM').val();
                var description = $('#txtDescM').val();
                var image = $('#txtImage').val();
                var content = CKEDITOR.instances.txtContent.getData();
                var seoMetaDescription = $('#txtMetaDescriptionM').val();
                var seoAlias = $('#txtSeoAliasM').val();
                var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;
                var hot = $('#ckHotM').prop('checked');
                var showHome = $('#ckShowHomeM').prop('checked');

                $.ajax({
                    type: "POST",
                    url: "/Admin/Blog/SaveEntity",
                    data: {
                        Id: id,
                        Name: name,
                        Image: image,
                        Description: description,
                        Content: content,
                        HomeFlag: showHome,
                        HotFlag: hot,
                        Status: status,
                        SeoAlias: seoAlias,
                        SeoDescription: seoMetaDescription
                    },
                    dataType: "json",
                    beforeSend: function () {
                        khoaluan.startLoading();
                    },
                    success: function (response) {
                        khoaluan.notify('Update product successful', 'success');
                        $('#modal-add-edit').modal('hide');
                        resetFormMaintainance();
                        khoaluan.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        khoaluan.notify('Has an error in save product progress', 'error');
                        khoaluan.stopLoading();
                    }
                });
                return false;
            }

        });
    }
    function deleteBlog(that) {
        khoaluan.confirm('Xác nhận xóa bài viết?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Blog/Delete",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    khoaluan.startLoading();
                },
                success: function (response) {
                    khoaluan.notify('Xóa thành công', 'success');
                    khoaluan.stopLoading();
                    loadData(true);
                },
                error: function (status) {
                    khoaluan.notify('Has an error in delete progress', 'error');
                    khoaluan.stopLoading();
                }
            });
        });
    }
    function registerControls() {
        CKEDITOR.replace('txtContent', {});

        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };

    }
    function loadData(isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                keyword: $('#txtKeyword').val(),
                page: khoaluan.configs.pageIndex,
                pageSize: khoaluan.configs.pageSize
            },
            url: '/Admin/Blog/GetAllPaging',
            dataType: 'json',
            success: function (response) {
                console.log(response);
                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        Content: item.Content,
                        Description: item.Description,
                        Image: item.Image == null ? '<img src="/admin-side/images/user.png" width=25' : '<img src="' + item.Image + '" width=25 />',
                        CreatedDate: khoaluan.dateTimeFormatJson(item.DateCreated),
                        Status: khoaluan.getStatus(item.Status)
                    });
                    $('#lblTotalRecords').text(response.RowCount);
                    if (render != '') {
                        $('#tbl-content').html(render);
                    }
                    wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);
                });
            },
            error: function (status) {
                console.log(status);
                khoaluan.notify('Cannot loading data', 'error');
            }
        });
    }
    function resetFormMaintainance() {

        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        $('#txtContent').val('');
        $('#txtDescM').val('');

        $('#txtHomeOrderM').val('');
        $('#txtImage').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoAliasM').val('');

        $('#ckStatusM').prop('checked', true);
        $('#ckHotM').prop('checked', true);
        $('#ckShowHomeM').prop('checked', false);
    }
    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / khoaluan.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                khoaluan.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
}