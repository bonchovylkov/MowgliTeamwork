/// <reference path="jquery-2.0.2.js" />


var ui = (function () {

	function buildLoginForm() {
		var html =
            '<div id="login-form-holder">' +
				'<form>' +
					'<div id="login-form">' +
						'<label for="tb-login-username">Username: </label>' +
						'<input type="text" id="tb-login-username"><br />' +
						'<label for="tb-login-password">Password: </label>' +
						'<input type="text" id="tb-login-password"><br />' +
						'<button id="btn-login" class="button">Login</button>' +
					'</div>' +
					'<div id="register-form" style="display: none">' +
						'<label for="tb-register-username">Username: </label>' +
						'<input type="text" id="tb-register-username"><br />' +
						'<label for="tb-register-password">Password: </label>' +
						'<input type="text" id="tb-register-password"><br />' +
						'<button id="btn-register" class="button">Register</button>' +
					'</div>' +
					'<a href="#" id="btn-show-login" class="button selected">Login</a>' +
					'<a href="#" id="btn-show-register" class="button">Register</a>' +
				'</form>' +
				'<div id="error-messages"></div>' +
            '</div>';
		return html;
	}

	function buildrecipeUI(nickname) {
	    var html = '<span id="user-nickname">Hello ' +
				nickname +
		'!</span>' +
		'<button id="btn-logout">Logout</button><br/>' +
        '<h1>User Recipies</h1>' +
		'<div id="userReps">' +

		'</div>' +
        '<h1>ALL Recipies</h1>' +
		'<div id="allRecipies">' +
			
		'</div>' +
		'<div id="add-recipe-container">' +
			'<h2>Add Recipe</h2>' +
            '<label for="RecipeName">Recipe Name: </label>' +
            '<input type="text" id="RecipeName"><br />' +
            '<label for="products">Recipe Products: </label>' +
            '<input type="text" id="products"><br />' +
            '<label for="pictures">Recipe Picture: </label>' +
            '<input name="image" type="file" id="pictures"><br />' +
            '<button id="upload-picture" class="button">Upload Picture</button>' +
            '<button id="add-recipe" class="button">Add Recipe</button>' +
		'</div>';
		return html;
	}

	function buildloadAllReps(Recipe) {
		var list = '<ul class="game-list open-Recipe">';
		for (var i = 0; i < Recipe.length; i++) {
			var rep = Recipe[i];
			list +=
				'<li data-game-id="' + rep.RecipeId + '" class="recipe">  Name:' +
					'<a href="#" >' +
						$("<div />").html(rep.RecipeName).text() +
					'</a>' +
					'<span> Made from ' +
						rep.Products +
					'</span>' +
				'</li>';
		}
		list += "</ul>";
		return list;
	}

	function buildActiveRecipeList(Recipe) {
		//var RecipeList = Array.prototype.slice.call(Recipe, 0);
		//RecipeList.sort(function (g1, g2) {
		//	if (g1.status == g2.status) {
		//		return g1.title > g2.title;
		//	}
		//	else {
		//		if (g1.status == "in-progress") {
		//			return -1;
		//		}
		//	}
		//	return 1;
		//});

	    var list = '<ul class="game-list open-Recipe">';
	    for (var i = 0; i < Recipe.length; i++) {
	        var rep = Recipe[i];
	        list +=
				'<li data-game-id="' + rep.RecipeId + '"  class="recipe">  Name:' +
					'<a href="#" >' +
						$("<div />").html(rep.RecipeName).text() +
					'</a>' +
					'<span> Made from ' +
						rep.Products +
					'</span>' +
				'</li>';
	    }
	    list += "</ul>";
	    return list;
	}

	function UploadLastRecipe(recipe) {
	 var   list =
				'<li data-game-id="' + recipe.RecipeId + '" class="recipe">  Name:' +
					'<a href="#" >' +
						$("<div />").html(recipe.RecipeName).text() +
					'</a>' +
					'<span> Made from ' +
						recipe.Products +
					'</span>' +
				'</li>';
	    return list;


	}

	//function buildGuessTable(guesses) {
	//	var tableHtml =
	//		'<table border="1" cellspacing="0" cellpadding="5">' +
	//			'<tr>' +
	//				'<th>Number</th>' +
	//				'<th>Cows</th>' +
	//				'<th>Bulls</th>' +
	//			'</tr>';
	//	for (var i = 0; i < guesses.length; i++) {
	//		var guess = guesses[i];
	//		tableHtml +=
	//			'<tr>' +
	//				'<td>' +
	//					guess.number +
	//				'</td>' +
	//				'<td>' +
	//					guess.cows +
	//				'</td>' +
	//				'<td>' +
	//					guess.bulls +
	//				'</td>' +
	//			'</tr>';
	//	}
	//	tableHtml += '</table>';
	//	return tableHtml;
	//}

	//function buildRecipetate(Recipetate) {
	//	var html =
	//		'<div id="game-state" data-game-id="' + Recipetate.id + '">' +
	//			'<h2>' + Recipetate.title + '</h2>' +
	//			'<div id="blue-guesses" class="guess-holder">' +
	//				'<h3>' +
	//					Recipetate.blue + '\'s gueesses' +
	//				'</h3>' +
	//				buildGuessTable(Recipetate.blueGuesses) +
	//			'</div>' +
	//			'<div id="red-guesses" class="guess-holder">' +
	//				'<h3>' +
	//					Recipetate.red + '\'s gueesses' +
	//				'</h3>' +
	//				buildGuessTable(Recipetate.redGuesses) +
	//			'</div>' +
	//	'</div>';
	//	return html;
	//}
	
	//function buildMessagesList(messages) {
	//	var list = '<ul class="messages-list">';
	//	var msg;
	//	for (var i = 0; i < messages.length; i += 1) {
	//		msg = messages[i];
	//		var item =
	//			'<li>' +
	//				'<a href="#" class="message-state-' + msg.state + '">' +
	//					msg.text +
	//				'</a>' +
	//			'</li>';
	//		list += item;
	//	}
	//	list += '</ul>';
	//	return list;
	//}

	return {
		recipeUI: buildrecipeUI,
		loadAllReps: buildloadAllReps,
		loginForm: buildLoginForm,
		recipeFromUser: buildActiveRecipeList,
	    AddRecipe:UploadLastRecipe
		//Recipetate: buildRecipetate,
		//messagesList: buildMessagesList
	}

}());