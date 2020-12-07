// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function keypressHandler(e) {
    if (e.which == 13) {
        e.preventDefault(); //stops default action: submitting form
        $(this).blur();
        $('#folderToolsName').focus().click();//give your submit an ID
    }
}

$('#folderToolsForm').keypress(keypressHandler);

<script type="text/javascript">
    $(window).on('load',function(){
        $('#myModal').modal('show');
    });
</script>