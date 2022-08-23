// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function rentOut(id){
    $.ajax({
        type: 'POST',
        url: window.location.origin + "/Surfboards/Rent/"+id,
        success: resp => {
            console.log(resp);
        }
    });
}