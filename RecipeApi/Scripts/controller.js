﻿/// <reference path="class.js" />
/// <reference path="persister.js" />
/// <reference path="jquery-2.0.2.js" />
/// <reference path="ui.js" />



//THE ORIGINAL ONE
var controllers = (function () {

	var updateTimer = null;

	var rootUrl = "http://mowgliteam.apphb.com/api/";
   // var rootUrl = "http://localhost:12789/api/";
	var imageUrl = "";

	var Controller = Class.create({
		init: function () {
			this.persister = persisters.get(rootUrl);
		},
		loadUI: function (selector) {
			if (this.persister.isUserLoggedIn()) {
				this.loadMainU(selector);
			}
			else {
				this.loadLoginFormUI(selector);
			}
			this.attachUIEventHandlers(selector);
		},
		loadLoginFormUI: function (selector) {
			var loginFormHtml = ui.loginForm()
			$(selector).html(loginFormHtml);
		},
		loadMainU: function (selector) {
			var self = this;
			var recipeUIHtml =
				ui.recipeUI(this.persister.nickname());
			$(selector).html(recipeUIHtml);

			this.updateUI(selector);

			//updateTimer = setInterval(function () {
			//	self.updateUI(selector);
			//}, 15000);
		},
		loadGame: function (selector, gameId) {
			this.persister.game.state(gameId, function (gameState) {
				var gameHtml = ui.gameState(gameState);
				$(selector + " #game-holder").html(gameHtml)
			});
		},
		attachUIEventHandlers: function (selector) {
		    var wrapper = $(selector);
		    var self = this;

		    wrapper.on("click", "#btn-show-login", function () {
		        wrapper.find(".button.selected").removeClass("selected");
		        $(this).addClass("selected");
		        wrapper.find("#login-form").show();
		        wrapper.find("#register-form").hide();
		    });
		    wrapper.on("click", "#btn-show-register", function () {
		        wrapper.find(".button.selected").removeClass("selected");
		        $(this).addClass("selected");
		        wrapper.find("#register-form").show();
		        wrapper.find("#login-form").hide();
		    });
		
			wrapper.on("click", "#btn-login", function () {
				var user = {
					username: $(selector + " #tb-login-username").val(),
					password: $(selector + " #tb-login-password").val()
				}

				self.persister.user.login(user, function () {
					self.loadMainU(selector);
				}, function (err) {
					wrapper.find("#error-messages").text(err.responseJSON.Message);
				});
				return false;
			});
			wrapper.on("click", "#btn-register", function () {
				var user = {
					username: $(selector).find("#tb-register-username").val(),
					password: $(selector + " #tb-register-password").val()
				}
				self.persister.user.register(user, function () {
					self.loadMainU(selector);
				}, function (err) {
					wrapper.find("#error-messages").text(err.responseJSON.Message);
				});
				return false;
			});
			wrapper.on("click", "#btn-logout", function () {
				self.persister.user.logout(function () {
					self.loadLoginFormUI(selector);
					//alert("sucsses");
				}, function (err) {
				    self.loadLoginFormUI(selector);
				});
			});

			wrapper.on("click", "#add-recipe", function () {
			    var recipe = {
			        RecipeName: $(selector).find("#RecipeName").val(),
			        Products: $(selector).find("#products").val(),
			        PictureLink: imageUrl
			    }

			    self.persister.recipe.addRecipe(recipe, function (data) {
			      var html =  ui.AddRecipe(data);
			      $(selector + " #allRecipies ul").append(html);
			      $(selector + " #userReps ul").append(html);
			        alert(JSON.stringify(data));    
			    }, function (err) {
			        alert(JSON.stringify(err));
			    });
			});

			wrapper.on("click", ".recipe", function () {
			    var recipeNumber = $(this).attr("data-game-id");

			    self.persister.recipe.getOneRecipe(recipeNumber, function (data) {
			        alert(JSON.stringify(data));
			    }, function (err) {
			        alert(JSON.stringify(err));
			    });
			});

			wrapper.on("click", "#upload-picture", function upload() {
			    var ourUploadUrl = rootUrl + "recipes/upload"

			    var data = new FormData();
			    jQuery.each($('#pictures')[0].files, function (i, file) {
			        data.append('file-' + i, file);
			    });

			    $.ajax({
			        url: ourUploadUrl,
			        data: data,
			        cache: false,
			        contentType: false,
			        processData: false,
			        type: 'POST',
			        success: function successFunc(data) {
			            imageUrl = data;
			        },
			        error: function errorFunc(data) {
			            alert("error " + JSON.stringify(data));
			        }
			    });
			});

		//	wrapper.on("click", "#open-games-container a", function () {
		//		$("#game-join-inputs").remove();
		//		var html =
		//			'<div id="game-join-inputs">' +
		//				'Number: <input type="text" id="tb-game-number"/><br/>' +
		//				'Password: <input type="text" id="tb-game-pass"/><br/>' +
		//				'<button id="btn-join-game">join</button>' +
		//			'</div>';
		//		$(this).after(html);
		//	});
		//	wrapper.on("click", "#btn-join-game", function () {
		//		var game = {
		//			number: $("#tb-game-number").val(),
		//			gameId: $(this).parents("li").first().data("game-id")
		//		};

		//		var password = $("#tb-game-pass").val();

		//		if (password) {
		//			game.password = password;
		//		}
		//		self.persister.game.join(game);
		//	});
		//	wrapper.on("click", "#btn-create-game", function () {
		//		var game = {
		//			title: $("#tb-create-title").val(),
		//			number: $("#tb-create-number").val(),
		//		}
		//		var password = $("#tb-create-pass").val();
		//		if (password) {
		//			game.password = password;
		//		}
		//		self.persister.game.create(game);
		//	});

		//	wrapper.on("click", "#active-games-container li.game-status-full a.btn-active-game", function () {
		//		var gameCreator = $(this).parent().data("creator");
		//		var myNickname = self.persister.nickname();
		//		if (gameCreator == myNickname) {
		//			$("#btn-game-start").remove();
		//			var html =
		//				'<a href="#" id="btn-game-start">' +
		//					'Start' +
		//				'</a>';
		//			$(this).parent().append(html);
		//		}
		//	});

		//	wrapper.on("click", "#btn-game-start", function () {
		//		var parent = $(this).parent();

		//		var gameId = parent.data("game-id");
		//		self.persister.game.start(gameId, function () {
		//			wrapper.find("#game-holder").html("started");
		//		},
		//		function (err) {
		//			alert(JSON.stringify(err));
		//		});

		//		return false;
		//	});
			
		//	wrapper.on("click", ".active-games .in-progress", function () {
		//		self.loadGame(selector, $(this).parent().data("game-id"));
		//	});
		},
		updateUI: function (selector) {
			this.persister.recipe.getAll(function (games) {
				var list = ui.loadAllReps(games);
			    $(selector + " #allRecipies")
					.html(list);
			});
			this.persister.recipe.getByUser(function (games) {
				var list = ui.recipeFromUser(games);
			    $(selector + " #userReps")
					.html(list);
			});
			//this.persister.message.all(function (msg) {
			//	var msgList = ui.messagesList(msg);
			//	$(selector + " #messages-holder").html(msgList);
			//});
		}
	});
	return {
		get: function () {
			return new Controller();
		}
	}
}());

$(function () {
	var controller = controllers.get();
	controller.loadUI("#content");
});