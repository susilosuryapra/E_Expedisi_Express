$('#saveDocumentBtn').on('click', function() {
    var form = document.getElementById('addDocumentForm');
    
    // Validasi form secara manual menggunakan HTML5 form validation API
    if (!form.checkValidity()) {
        form.reportValidity(); // Menampilkan pesan validasi bawaan HTML5
        return; // Hentikan eksekusi jika form tidak valid
    }

    var formData = {
        code: $('#code').val(),
        description: $('#description').val(),
        status: $('#status').val() === "1" // Convert to boolean
    };

    // Lakukan AJAX untuk menyimpan data
    $.ajax({
        type: 'POST',
        url: '/DType/AddDocumentType', // Sesuaikan dengan URL API untuk menambahkan data
        data: JSON.stringify(formData),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function(response) {
            // Logika jika berhasil
            if (response.success) {
                Swal.fire({
                    title: 'Success!!',
                    text: 'Document Type has been added succesfully!',
                    icon: 'success',
                    confirmButtonText: 'OK'
                }).then(() => {
                    location.reload();
                });
            } else {
                Swal.fire(
                    'Error!',
                    response.message,
                    'error'
                );
            }
        },
        error: function() {
            Swal.fire(
                'Error!',
                'There was an issue adding the document type.',
                'error'
            );
        }
    });
});
