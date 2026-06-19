/* =============================================
   RentCar Admin Panel — admin.js
   ============================================= */

// ===== SIDEBAR =====
function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    const wrapper = document.getElementById('mainWrapper');
    const overlay = document.getElementById('sidebarOverlay');
    const isMobile = window.innerWidth <= 768;

    if (isMobile) {
        sidebar.classList.toggle('mobile-open');
        overlay.classList.toggle('show');
    } else {
        sidebar.classList.toggle('collapsed');
    }
}

function closeSidebar() {
    document.getElementById('sidebar').classList.remove('mobile-open');
    document.getElementById('sidebarOverlay').classList.remove('show');
}

// Ekran boyutu değiştiğinde düzelt
window.addEventListener('resize', () => {
    if (window.innerWidth > 768) {
        document.getElementById('sidebar').classList.remove('mobile-open');
        document.getElementById('sidebarOverlay').classList.remove('show');
    }
});

// ===== ACTIVE NAV ITEM =====
// URL'e göre otomatik aktif menü
(function () {
    const currentPath = window.location.pathname.toLowerCase();
    document.querySelectorAll('.nav-item').forEach(link => {
        const href = link.getAttribute('href');
        if (href && currentPath.includes(href.toLowerCase()) && href !== '/') {
            link.classList.add('active');
        }
    });
})();

// ===== TOAST NOTIFICATION =====
function showToast(message, type = 'success') {
    const container = document.getElementById('toastContainer');
    if (!container) return;

    const icon = type === 'success' ? 'ti-circle-check' : 'ti-circle-x';
    const toast = document.createElement('div');
    toast.className = `toast-custom ${type} mb-2`;
    toast.innerHTML = `
        <i class="ti ${icon}"></i>
        <span class="toast-text">${message}</span>
    `;

    container.appendChild(toast);

    // Bootstrap toast animasyonu
    toast.style.opacity = '0';
    toast.style.transform = 'translateY(10px)';
    toast.style.transition = 'all 0.3s ease';

    setTimeout(() => {
        toast.style.opacity = '1';
        toast.style.transform = 'translateY(0)';
    }, 10);

    setTimeout(() => {
        toast.style.opacity = '0';
        toast.style.transform = 'translateY(10px)';
        setTimeout(() => toast.remove(), 300);
    }, 3500);
}

// ===== DELETE CONFIRM =====
function confirmDelete(url, itemName = 'bu kaydı') {
    if (confirm(`"${itemName}" silmek istediğinizden emin misiniz?`)) {
        fetch(url, { method: 'POST' })
            .then(res => {
                if (res.ok) {
                    showToast('Kayıt başarıyla silindi.', 'success');
                    setTimeout(() => window.location.reload(), 1000);
                } else {
                    showToast('Silme işlemi başarısız.', 'error');
                }
            })
            .catch(() => showToast('Bir hata oluştu.', 'error'));
    }
}

// ===== TABLE SEARCH (client-side) =====
function initTableSearch(inputId, tableId) {
    const input = document.getElementById(inputId);
    const table = document.getElementById(tableId);
    if (!input || !table) return;

    input.addEventListener('input', () => {
        const filter = input.value.toLowerCase();
        table.querySelectorAll('tbody tr').forEach(row => {
            const text = row.textContent.toLowerCase();
            row.style.display = text.includes(filter) ? '' : 'none';
        });
    });
}

// ===== IMAGE PREVIEW =====
function initImagePreview(inputId, previewId) {
    const input = document.getElementById(inputId);
    const preview = document.getElementById(previewId);
    if (!input || !preview) return;

    input.addEventListener('change', () => {
        const file = input.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = e => {
                preview.src = e.target.result;
                preview.style.display = 'block';
            };
            reader.readAsDataURL(file);
        }
    });
}

// ===== RESERVATION STATUS UPDATE =====
function updateReservationStatus(rentalId, status) {
    fetch(`/Admin/Rental/UpdateStatus`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ rentalId, status })
    })
        .then(res => res.json())
        .then(data => {
            if (data.success) {
                showToast(
                    status === 'Onaylandı'
                        ? 'Rezervasyon onaylandı, mail gönderildi.'
                        : 'Rezervasyon reddedildi.',
                    'success'
                );
                setTimeout(() => window.location.reload(), 1200);
            } else {
                showToast('İşlem başarısız.', 'error');
            }
        })
        .catch(() => showToast('Bir hata oluştu.', 'error'));
}
