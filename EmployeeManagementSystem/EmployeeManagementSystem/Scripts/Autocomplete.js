$(function () {
        $('.name').autocomplete({
            source: function(request, response) {
                     $.ajax({
                         url: '@Url.Action("GetCandidateDetail","Interview")',
                         data: "{ 'name': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                         success: function (data) {
                             response($.map(data, function (item) {
                                 return item;
                                }))
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                 select: function (event, ui) {
                     $('.name').val(ui.item.Name);
                     $('#CandidateId').val(ui.item.Id);
                     return false;
                    },
                 minLength: 3
             }).autocomplete("instance")._renderItem = function (ul, item) {
                    return $( "<li>" )
                        .append("<div>" + "<i class='fa fa-user'> " + "<b>" + "Name :  " + "</b>" + "</i>" + item.Name + "<br>" + "<i class = 'fa fa-phone'> " + "<b>" + "Mobile No : " + "</b>" + item.MobileNo + "</i>" + "<br>" + "<i class = 'fa fa-envelope' > " + "<b>" + "Email : " + "</b>" + item.Email + "</i>" + "</div>" )
                    .appendTo( ul );
            };
        });