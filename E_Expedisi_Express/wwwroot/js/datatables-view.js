var js = jQuery.noConflict(true);
js(document).ready(function() {
    js('#documentTypeTable').DataTable({
        ajax: {
            url: '/DType/GetDocumentTypes', // Sesuaikan dengan controller Anda
            type: 'GET',
            dataType: 'json',
            error: function(jqXHR, textStatus, errorThrown) {
                console.error("DataTable Error:", textStatus, errorThrown);
                Swal.fire('Error!', 'Terjadi kesalahan saat mengambil data.', 'error');
            },
            dataSrc: '' // Pastikan ini sesuai dengan format JSON yang dikembalikan
        },
        columns: [
            { data: 'id' },
            { data: 'code' },
            { data: 'description' },
            { data: 'status', render: function(data) { return data ? 'Active' : 'Inactive'; } },
            {
                data: null,
                render: function(data) {
                    return `<button class="btn btn-info" onclick="viewDetail(${data.id})">View Detail</button>`;
                }
            },
        ],
        language: {
            search: '',
        },
        initComplete: function() {
            var input = this.api().table().container().querySelector('input[type="search"]');
            if (input) {
                input.id = 'searchInput';
                input.classList.add('form-control');
                input.placeholder = 'Cari Data';
            }
        }
    });
});

// Fungsi untuk melihat detail
function viewDetail(id) {
    $.ajax({
        url: '/DType/GetDocumentType/' + id, // Pastikan ini sesuai dengan URL untuk mendapatkan detail
        type: 'GET',
        success: function(data) {
            // Tampilkan data dalam modal atau cara lain
            Swal.fire({
                title: 'Detail Document Type',
                html: `
                    <strong>ID:</strong> ${data.id}<br>
                    <strong>Code:</strong> ${data.code}<br>
                    <strong>Description:</strong> ${data.description}<br>
                    <strong>Status:</strong> ${data.status ? 'Active' : 'Inactive'}
                `,
                confirmButtonText: 'Tutup'
            });
        },
        error: function() {
            Swal.fire('Error!', 'Terjadi kesalahan saat mengambil detail.', 'error');
        }
    });
}
