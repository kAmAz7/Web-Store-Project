
$(document).ready(function () {
    $('.prodBox').click(function () {
        var listItem = $(this).parent().parent();
        var pId = $(this).attr('Value');
        var myURL = 'http://'+window.location.host+'/Product/RemoveProduct/';
        $.post(myURL, { productId: pId }, function (productId) {
            $(listItem).remove();
            location.reload();         
        });
        });
});

