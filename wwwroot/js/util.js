function showModal(id) {
	$("#" + id).show();
}

$(document).ready(function () {
	if (sessionStorage.curpos) {
		$("main").scrollTop(sessionStorage.curpos);
		sessionStorage.removeItem("curpos");
	}

	$(".cancel").click(function () {
		sessionStorage.curpos = $("main").scrollTop();
		window.location.reload();
	});


	$("form:not(.formAjax)").submit(function (e) {
		$(this).find("input[type=submit]")
			.attr("disabled", true);
	});

	$(".formAjax").submit(function (e) {
		e.preventDefault();

		var input =
			$(this).find("input[type=submit]")
				.attr("disabled", true);

		var actionUrl = $(this).attr("action");
		$(".error").text("");

		sessionStorage.curpos = $("main").scrollTop();

		$.post(actionUrl, $(this).serialize(), function (res) {
			if (res == true) {
				window.location.reload();
			} else {
				$(".error").html(res);
				input.attr("disabled", false);
			}
		});
	});

	$(".formAjaxWithFile").submit(function (e) {
		e.preventDefault();

		var input =
			$(this).find("input[type=submit]")
				.attr("disabled", true);

		var actionUrl = $(this).attr("action");
		var method = $(this).attr("method");

		$(".error").text("");

		sessionStorage.curpos = $("main").scrollTop();

		var form = $(this).closest("form");
		var formData = new FormData(form[0]);

		$.ajax({
			type: method,
			data: formData,
			dataType: "json",
			url: actionUrl,
			processData: false,
			contentType: false,
			success: function (res) {
				if (res == true) {
					window.location.reload();
				} else {
					$(".error").html(res);
					input.attr("disabled", false);
				}
			}

		});
	});
});