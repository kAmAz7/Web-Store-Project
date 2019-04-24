$.validator.addMethod('noWeekends', function (value, element) {
    try {
        var d = $.datepick.parseDate('mm/dd/yyyy', value);
        return this.optional(element) || (d.getDay() || 7) < 6;
    }
    catch (e) {
        return true; // Will be validated elsewhere
    }
},
    'Cannot choose a weekend');

$(function () {
    $('#date').datepick({ onDate: $.datepick.noWeekends });
    $('form').validate({
        rules: {
            date: {
                noWeekends: true,
                dpDate: true
            }
        }
    });
});