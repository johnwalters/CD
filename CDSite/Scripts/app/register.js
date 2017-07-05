$(document).ready(function () {
    $("button#submitEmail").click(function () {
        $.get("/api/register", {
            email: $("input#EmailSubmit").val(),
            offerToken: $("input#OfferToken").val()
        }, function (data, status) {
            if (data.IsSuccessful == true) {
                $("div#Message").html(data.SuccessMessage);
            }
            else {
                $("div#Message").html(data.ErrorMessages[0]);
            }
        });
    });
});
//# sourceMappingURL=register.js.map