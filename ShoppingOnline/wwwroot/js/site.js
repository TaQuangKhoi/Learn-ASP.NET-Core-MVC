// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import tippy from 'tippy.js';
import 'tippy.js/dist/tippy.css'; // optional for styling

tippy('#tag-a-open-update', {
    content: 'My tooltip!',
});

function  OnChangeQuantity( id, e ) {
    var  quantity = $( "#quantity-" + id ).val();
    var  price = $( "#price-" + id ).val();
    var  total = quantity * price;
    $( "#total-" + id ).text( total );
}