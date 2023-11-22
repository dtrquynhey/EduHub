/* eslint-disable no-unused-vars */
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//function selectRole (role) {
//  document.getElementById('Role').value = role;
//  document.querySelector('.btn-secondary').textContent = role;
//}

function loadComments(url, campaignId) {
    $.ajax({
        url: url,
        type: 'GET',
        data: { campaignId: campaignId },
        success: function (data) {
            $('#commentsContainer-' + campaignId).html(data);
        }
    });
}

function comment(url, token, campaignId, commentContent) {
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            __RequestVerificationToken: token,
            campaignId: campaignId,
            content: commentContent
        },
        success: function (data) {
            $('#commentsContainer-' + campaignId).html(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error('Error adding comment: ' + textStatus, errorThrown);
        }
    });
}



