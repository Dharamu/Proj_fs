
$(document).ready(function () {

    var weightG = {
        "100": "100 Gram",
        "250": "250 Gram",
        "500": "500 Gram",
        "1": "1 Kg",
        "2.5": "2.5 Kg",
        "5": "5 Kg"
    }

    var weightKg = {
        "1": "1 Kg",
        "2": "2 Kg",
        "5": "5 Kg",
        "10": "10 Kg",
        "50": "50 Kg"
    }
    var weightdozen = {
        "1 Dozen": "1 Dozen",
        "2 Dozen": "2 Dozen",
        "5 Dozen": "5 Dozen",
        "10 Dozen": "10 Dozen",
        "50 Dozen": "50 Dozen"
    }



    $(".example_d").click(function () {
        var $atest = $(this).find('.id').val();
        var $name = $(this).parent().find('.name').text();
        var $price = $(this).parent().find('.price').text();
        var $weigthcat = $(this).parent().find('.price .weigthcat').text();
        var $Image = $(this).parent().find('.image').attr('src');
        //alert($Image);

        if ($.trim($weigthcat) == 'Kg') {
            selectValues = weightKg;
        }
        else if ($.trim($weigthcat) == 'Gram') {
            selectValues = weightG;
        }
        else {
            selectValues = weightdozen;
        }


        var $mySelect = $('#Quantity');
        $mySelect.empty();
        $.each(selectValues, function (key, value) {
            var $option = $("<option/>", {
                value: key,
                text: value
            });
            $mySelect.append($option);
        });
        //var selectValues;
        //selectValues = weightKg;
        $("#id").val($atest);
        $("#fname").text($name);
        $("#fprice").text($price);
        $("#image").attr('src', $Image)

    });

    //$(".example_d").click(function () {
    //    var $atest = $(this).find('.id').val();
    //    var $name = $(this).parent().find('.name').text();
    //    var $price = $(this).parent().find('.price').text();
    //    var $weigthcat = $(this).parent().find('.price .weigthcat').text();
    //    var $Image = $(this).parent().find('.image').attr('src');
    //    //alert($Image);

    //    if ($.trim($weigthcat) == 'Kg') {
    //        selectValues = weightKg;
    //    }
    //    else if ($.trim($weigthcat) == 'Gram') {
    //        selectValues = weightG;
    //    }
    //    else {
    //        selectValues = weightdozen;
    //    }


    //    var $mySelect = $('#Quantity');
    //    $mySelect.empty();
    //    $.each(selectValues, function (key, value) {
    //        var $option = $("<option/>", {
    //            value: key,
    //            text: value
    //        });
    //        $mySelect.append($option);
    //    });
    //    //var selectValues;
    //    //selectValues = weightKg;
    //    $("#id").val($atest);
    //    $("#fname").text($name);
    //    $("#fprice").text($price);
    //    $("#image").attr('src', $Image)

    //});

    $(".edt").click(function () {
        //alert('jbjb');
        var $Image = $(this).parent().parent().find('td .pimage').attr('src');
        var $id = $(this).parent('td').find('input').val();
        var $price = $(this).parent().parent().find('td .price, .p').text().trim();
        var $quantity = $(this).parent().parent().find('td .quantity').text();
        var $weigthcat = $(this).parent().parent().find('td .wit').text().trim();

        

        if ($.trim($weigthcat) == 'Kg') {
            selectValues = weightKg;
        }
        else if ($.trim($weigthcat) == 'Gram') {
            selectValues = weightG;
        }
        else {
            selectValues = weightdozen;
        }


        var $mySelect = $('#Quantity');
        $mySelect.empty();
        $.each(selectValues, function (key, value) {
            var $option = $("<option/>", {
                value: key,
                text: value
            });
            $mySelect.append($option);
        });
        $("#id").val($id);
        $("#fname").text('vbb');
        $("#fprice").text($price);
        $("#imagecrt").attr('src', $Image)
        $("#exampleModal").show();
        
    });





});



//$.fn.myFunction2 = function () {
//    alert('You have successfully defined the function!');
//}
//$(".call-btn").click(function () {
//    $.fn.myFunction2();
//});


//$("#tbUser").on('click', '.btnDelete', function () {
//    $(this).closest('tr').remove();
//});