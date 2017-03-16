$(document).ready(function () {
    bars();
})

function bars() {
    var calories = '';
    var protein = '';
    var fat = '';
    var carbs = '';
    $.ajax({
        url: "../Users/getMaxMacros",
        type: "GET",
        success: function (data) {
            console.log(data);
            calories += '<div class="progress-bar" role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100" style="width:' + data[4] + '%; background-color: #b81d18">';
            calories += '<p style="text-align: left; color: #202020; letter-spacing:1px; font-weight:bold;">' + data[8] + '/' + data[0] + ' ' + data[4] + '% of your daily caloric intake</p></div>';
            protein += ' <div class="progress-bar" role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100" style="width:' + data[5] + '%; background-color: #b81d18">';
            protein += '<p style="text-align: left; color: #202020; letter-spacing:1px; font-weight:bold;">' + data[9] + '/' + data[1] + ' ' + data[5] + '% of your daily protein intake</p></div>';
            carbs += ' <div class="progress-bar" role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100" style="width:' + data[7] + '%; background-color: #b81d18">';
            carbs += '<p style="text-align: left; color: #202020; letter-spacing:1px; font-weight:bold;">' + data[11] + '/' + data[3] + ' ' + data[7] + '% of your daily carb intake</p></div>';
            fat += ' <div class="progress-bar" role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100" style="width:' + data[6] + '%; background-color: #b81d18">';
            fat += '<p style="text-align: left; color: #202020; letter-spacing:1px; font-weight:bold;">' + data[10] + '/' + data[2] + ' ' + data[6] + '% of your daily fat intake</p></div>';
            $('#calorieBar').html(calories);
            $('#proteinBar').html(protein);
            $('#carbBar').html(carbs);
            $('#fatBar').html(fat);
        }
    })
}

function Search() {
    var displayInfo = '';
    var UserSearch = $('#search-keyword').val();
    var returnObj;
    var Url = "https://trackapi.nutritionix.com/v2/natural/nutrients";
    var meal = {
        "async": true,
        "crossDomain": true,
        "url": "" + Url,
        "method": "POST",
        "headers": {
            "content-type": "application/json",
            "accept": "application/json",
            "x-app-id": "15d7e8ca",
            "x-app-key": "b87fefe221a8b57acbd70ca8226aea80",
            "x-remote-user-id": "0",
            "cache-control": "no-cache",
        },
        "processData": false,
        "data": "{\n  \"query\": \"" + UserSearch + "\",\n  \"num_servings\": 1,\n  \"aggregate\": \"string\",\n  \"line_delimited\": false,\n  \"timezone\": \"US/Eastern\",\n  \"consumed_at\": null,\n  \"lat\": null,\n  \"lng\": null,\n  \"meal_type\": 0\n}"
    }

    $.ajax(meal).done(function (response) {
        console.log(response);
        var array = [];
        array[0] = response.foods[0];
        getArray(array);
    })

}


function getArray(Array) {
    var array = [];
    array[0] = Array[0];
    var macros = {
        calories: array[0].nf_calories,
        protein: array[0].nf_protein,
        fat: array[0].nf_total_fat,
        carbs: array[0].nf_total_carbohydrate,
        servingSize: array[0].serving_weight_grams
    }
    console.log(macros);
    getMacros(macros);
}

function getMacros(macros) {
    var displayInfo = '';
    var macros = macros;
    displayInfo += '<div class="row"><div class="col-md-8"><div class="row" style="height: 20%; width: 100%;"> Results: <div id="foodName">' + $('#search-keyword').val() + '</div>';
    displayInfo += '<divclass="row" style="height: 20%; width: 100%;"> Calories:<div id="foodCals">' + macros.calories + '</div></div>';
    displayInfo += '<div class="row" style="height: 20%; width: 100%;"> Protein Content:<div id="foodPro">' + macros.protein + '</div></div>';
    displayInfo += '<div class="row" style="height: 20%; width: 100%;"> Total Fat content:<div id="foodFat"> ' + macros.fat + '</div></div>';
    displayInfo += '<div class="row" style="height: 20%; width: 100%;"> Carb Content:<div id="foodCarbs">' + macros.carbs + '</div></div>';
    displayInfo += '<div class="row" style="height: 20%; width: 100%;"> Serving Size(grams):<div id="foodServing">' + macros.servingSize + '</div></div></div>';
    displayInfo += '<div class="col-md-3"><div class="row"><button type="button" onclick="saveToMeal1()" style="color:black; border-radius: 5px;">Add to Your First Meal</button></div><br>';
    displayInfo += '<div class="row"><button type="button" onclick="saveToSnack1()" style="color:black; border-radius: 5px;">Add Item to Your First Snack</button></div><br>';
    displayInfo += '<div class="row"><button type="button" onclick="saveToMeal2()" style="color:black; border-radius: 5px;">Add Item to Your Second Meal</button></div><br>';
    displayInfo += '<div class="row"><button type="button" onclick="saveToSnack2()" style="color:black; border-radius: 5px;">Add Item to Your Second Snack</button></div><br>';
    displayInfo += '<div class="row"><button type="button" onclick="saveToMeal3()" style="color:black; border-radius: 5px;">Add Item to Your Last Meal</button></div></div><br>';
    return $('#meal1').html(displayInfo);
}
function loadMeals() {
    getMeal1();
    getSnack1();
    getMeal2();
    getSnack2();
    getMeal3();
}

function saveToMeal1() {
    var foodName = $('#search-keyword').val();
    var foodCals = $('#foodCals').text();
    var foodPro = $('#foodPro').text();
    var foodFat = $('#foodFat').text();
    var foodCarbs = $('#foodCarbs').text();
    var foodServing = $('#foodServing').text();
    $.ajax({
        url: "../MealPlans/saveToMeal1",
        type: "POST",
        data: { 'name': foodName, 'cals': foodCals, 'protein': foodPro, 'fat': foodFat, 'carbs': foodCarbs, 'serving': foodServing },
        success: function (data) {
            console.log(data);
            loadMeals();
            bars();
        }
    });

}
function getMeal1() {
    var meal1Display = ''
    var newData = [];
    $.ajax({
        url: "../MealPlans/getMeal1",
        type: "GET",
        success: function (data) {
            console.log("THIS ONE" + data);
            for (var i = 0; i < data.length; i++) {
                newData[i] = data[i];
            }
            for (var i = 0; i < newData.length; i++) {
                meal1Display += '<input type="hidden" id="meal1Id" value="' + newData[i].Id + '"/>';
                meal1Display += '<div class="row" style="height: auto;">Food Item: ' + newData[i].FoodItem + '</div><div class="row">Serving Size(grams): ' + newData[i].servingSize + '</div>';
                meal1Display += '<button type="button" onClick="deleteMeal1(\'' + newData[i].Id + '\')" style="color:black; font-weight: bold; border-radius: 5px;">Remove</button>';
            }
            return $('#meal1Display').html(meal1Display);
        }
    })
}
function saveToSnack1() {
    var foodName = $('#search-keyword').val();
    var foodCals = $('#foodCals').text();
    var foodPro = $('#foodPro').text();
    var foodFat = $('#foodFat').text();
    var foodCarbs = $('#foodCarbs').text();
    var foodServing = $('#foodServing').text();
    $.ajax({
        url: "../MealPlans/saveToSnack1",
        type: "POST",
        data: { 'name': foodName, 'cals': foodCals, 'protein': foodPro, 'fat': foodFat, 'carbs': foodCarbs, 'serving': foodServing },
        success: function (data) {
            console.log(data);
            loadMeals();
            bars();
        }
    });

}
function getSnack1() {
    var meal1Display = ''
    var newData = [];
    $.ajax({
        url: "../MealPlans/getSnack1",
        type: "GET",
        success: function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                newData[i] = data[i];
            }
            for (var i = 0; i < newData.length; i++) {
                meal1Display += '<input type="hidden" id="meal1Id" value="' + newData[i].Id + '"/>';
                meal1Display += '<div class="row" style="height: auto;">Food Item: ' + newData[i].FoodItem + '</div><div class="row">Serving Size(grams): ' + newData[i].servingSize + '</div>';
                meal1Display += '<button type="button" onClick="deleteSnack1(\'' + newData[i].Id + '\')" style="color:black; font-weight: bold; border-radius: 5px;">Remove</button>';
            }
            return $('#snack1Display').html(meal1Display);
        }
    })
}
function saveToMeal2() {
    var foodName = $('#search-keyword').val();
    var foodCals = $('#foodCals').text();
    var foodPro = $('#foodPro').text();
    var foodFat = $('#foodFat').text();
    var foodCarbs = $('#foodCarbs').text();
    var foodServing = $('#foodServing').text();
    $.ajax({
        url: "../MealPlans/saveToMeal2",
        type: "POST",
        data: { 'name': foodName, 'cals': foodCals, 'protein': foodPro, 'fat': foodFat, 'carbs': foodCarbs, 'serving': foodServing },
        success: function (data) {
            console.log(data);
            loadMeals();
            bars();
        }
    });

}
function getMeal2() {
    var meal1Display = ''
    var newData = [];
    $.ajax({
        url: "../MealPlans/getMeal2",
        type: "GET",
        success: function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                newData[i] = data[i];
            }
            for (var i = 0; i < newData.length; i++) {
                meal1Display += '<input type="hidden" id="meal1Id" value="' + newData[i].Id + '"/>';
                meal1Display += '<div class="row" style="height: auto;">Food Item: ' + newData[i].FoodItem + '</div><div class="row">Serving Size(grams): ' + newData[i].servingSize + '</div>';
                meal1Display += '<button type="button" onClick="deleteMeal2(\'' + newData[i].Id + '\')" style="color:black; font-weight: bold; border-radius: 5px;">Remove</button>';
            }
            return $('#meal2Display').html(meal1Display);
        }
    })
}
function saveToSnack2() {
    var foodName = $('#search-keyword').val();
    var foodCals = $('#foodCals').text();
    var foodPro = $('#foodPro').text();
    var foodFat = $('#foodFat').text();
    var foodCarbs = $('#foodCarbs').text();
    var foodServing = $('#foodServing').text();
    $.ajax({
        url: "../MealPlans/saveToSnack2",
        type: "POST",
        data: { 'name': foodName, 'cals': foodCals, 'protein': foodPro, 'fat': foodFat, 'carbs': foodCarbs, 'serving': foodServing },
        success: function (data) {
            console.log(data);
            loadMeals();
            bars();
        }
    });

}
function getSnack2() {
    var meal1Display = ''
    var newData = [];
    $.ajax({
        url: "../MealPlans/getSnack2",
        type: "GET",
        success: function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                newData[i] = data[i];
            }
            for (var i = 0; i < newData.length; i++) {
                meal1Display += '<input type="hidden" id="meal1Id" value="' + newData[i].Id + '"/>';
                meal1Display += '<div class="row" style="height: auto;">Food Item: ' + newData[i].FoodItem + '</div><div class="row">Serving Size(grams): ' + newData[i].servingSize + '</div>';
                meal1Display += '<button type="button" onClick="deleteSnack2(\'' + newData[i].Id + '\')" style="color:black; font-weight: bold; border-radius: 5px;">Remove</button>';
            }
            return $('#snack2Display').html(meal1Display);
        }
    })
}
function saveToMeal3() {
    var foodName = $('#search-keyword').val();
    var foodCals = $('#foodCals').text();
    var foodPro = $('#foodPro').text();
    var foodFat = $('#foodFat').text();
    var foodCarbs = $('#foodCarbs').text();
    var foodServing = $('#foodServing').text();
    $.ajax({
        url: "../MealPlans/saveToMeal3",
        type: "POST",
        data: { 'name': foodName, 'cals': foodCals, 'protein': foodPro, 'fat': foodFat, 'carbs': foodCarbs, 'serving': foodServing },
        success: function (data) {
            console.log(data);
            loadMeals();
            bars();
        }
    });

}
function getMeal3() {
    var meal1Display = ''
    var newData = [];
    $.ajax({
        url: "../MealPlans/getMeal3",
        type: "GET",
        success: function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                newData[i] = data[i];
            }
            for (var i = 0; i < newData.length; i++) {
                meal1Display += '<input type="hidden" id="meal1Id" value="' + newData[i].Id + '"/>';
                meal1Display += '<div class="row" style="height: auto;">Food Item: ' + newData[i].FoodItem + '</div><div class="row">Serving Size(grams): ' + newData[i].servingSize + '</div>';
                meal1Display += '<button type="button" onClick="deleteMeal3(\'' + newData[i].Id + '\')" style="color:black; font-weight: bold; border-radius: 5px;">Remove</button>';
            }
            return $('#meal3Display').html(meal1Display);
        }
    })
}

function deleteMeal1(Id) {
    console.log(Id);
    $.ajax({
        url: "../MealPlans/deleteMeal1Item",
        type: "POST",
        data: { 'id': Id },
        success: function (data) {
            console.log(data);
            loadMeals();
            bars();
        }

    })

}

function deleteMeal2(Id) {
    console.log(Id);
    $.ajax({
        url: "../MealPlans/deleteMeal2Item",
        type: "POST",
        data: { 'id': Id },
        success: function (data) {
            console.log(data);
            loadMeals();
            bars();
        }
        
    })

}
function deleteMeal3(Id) {
    console.log(Id);
    $.ajax({
        url: "../MealPlans/deleteMeal3Item",
        type: "POST",
        data: { 'id': Id },
        success: function (data) {
            loadMeals();
            bars();
        }
    })
}
function deleteSnack1(Id) {
    console.log(Id);
    $.ajax({
        url: "../MealPlans/deleteSnack1Item",
        type: "POST",
        data: { 'id': Id },
        success: function (data) {
            loadMeals();
            bars();
        }
    })
}
function deleteSnack2(Id) {
    console.log(Id);
    $.ajax({
        url: "../MealPlans/deleteSnack2Item",
        type: "POST",
        data: { 'id': Id },
        success: function (data) {
            loadMeals();
            bars();
        }
    })
}