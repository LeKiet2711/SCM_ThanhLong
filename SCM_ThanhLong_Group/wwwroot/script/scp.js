<script>
    function showBackdrop() {
        var backdrop = document.createElement('div');
    backdrop.className = 'modal-backdrop-custom';
    document.body.appendChild(backdrop);
    }

    function hideBackdrop() {
        var backdrop = document.querySelector('.modal-backdrop-custom');
    if (backdrop) {
        backdrop.remove();
        }
    }
</script>