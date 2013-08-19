var ui = (function () {

	function buildLoginForm() {
		var html =
            '<div id="login-form-holder">' +
				'<form>' +
					'<div id="login-form">' +
						'<label for="tb-login-username">Username: </label>' +
						'<input type="text" id="tb-login-username"><br />' +
						'<label for="tb-login-password">Password: </label>' +
						'<input type="password" id="tb-login-password"><br />' +
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

	function buildRecipeUI(nickname) {
		var html = '<span id="user-nickname">' +
				nickname +
		'</span>' +
		'<button id="btn-logout">Logout</button><br/>' +
		'<div id="create-recipe-holder">' +
			'Name: <input type="text" id="tb-create-name" />' +
			'Products: <input type="text" id="tb-create-products" />' +
			'Steps: <input type="text" id="tb-create-steps" />' +
			'<button id="btn-create-recipe">Create</button>' +
		'</div>' +
		'<div id="all-recipes-container">' +
			'<h2>All recipes</h2>' +
			'<div id="all-recipes"></div>' +
		'</div>' +
		'<div id="my-recipes-container">' +
			'<h2>My recipes</h2>' +
			'<div id="my-recipes"></div>' +
		'</div>' +
		'<div id="recipe-holder">' +
		'</div>' +
		'<div id="messages-holder">' +
		'</div>';

		return html;
	}

	//function buildloadAllReps(games) {
	//	var list = '<ul class="game-list open-games">';
	//	for (var i = 0; i < games.length; i++) {
	//		var game = games[i];
	//		list +=
	//			'<li data-game-id="' + game.id + '">' +
	//				'<a href="#" >' +
	//					$("<div />").html(game.title).text() +
	//				'</a>' +
	//				'<span> by ' +
	//					game.creatorNickname +
	//				'</span>' +
	//			'</li>';
	//	}
	//	list += "</ul>";
	//	return list;
	//}

	function buildRecipeFromUser(recipes) {
	    var list = '<ul class="recipes-list user-recipes">';

	    for (var i = 0; i < recipes.length; i++) {
	        var recipe = recipes[i];
			list +=
				'<li recipe-id="' + recipe.id + '" recipe-creator="' + recipe.creatorNickname + '">' +
					'<a href="#" class="btn-recipe">' +
						$("<div />").html(recipe.title).text() +
					'</a>' +
					'<span> by ' +
						game.creatorNickname +
					'</span>' +
				'</li>';
		}
	    list += "</ul>";

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

	//function buildGameState(gameState) {
	//	var html =
	//		'<div id="game-state" data-game-id="' + gameState.id + '">' +
	//			'<h2>' + gameState.title + '</h2>' +
	//			'<div id="blue-guesses" class="guess-holder">' +
	//				'<h3>' +
	//					gameState.blue + '\'s gueesses' +
	//				'</h3>' +
	//				buildGuessTable(gameState.blueGuesses) +
	//			'</div>' +
	//			'<div id="red-guesses" class="guess-holder">' +
	//				'<h3>' +
	//					gameState.red + '\'s gueesses' +
	//				'</h3>' +
	//				buildGuessTable(gameState.redGuesses) +
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
		recipeUI: buildRecipeUI,
		//loadAllReps: buildloadAllReps,
		loginForm: buildLoginForm
		//recipeFromUser: buildrecipeFromUser,
		//gameState: buildGameState,
		//messagesList: buildMessagesList
	}

}());