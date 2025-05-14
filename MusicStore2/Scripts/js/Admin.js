// Admin.js - Script utilizat pentru paginile administrative

// Funcție pentru confirmare ștergere
function confirmDelete(message, callback) {
    if (confirm(message)) {
        callback();
    }
}

// Funcție pentru actualizare preț produs
function updateProductPrice(productId, price, updateUrl) {
    $.ajax({
        url: updateUrl,
        type: "POST",
        data: { id: productId, price: price },
        success: function(response) {
            if (response.success) {
                showNotification("Price updated successfully", "success");
            } else {
                showNotification("Failed to update price", "error");
            }
        },
        error: function() {
            showNotification("An error occurred while updating price", "error");
        }
    });
}

// Funcție pentru actualizare rol utilizator
function updateUserRole(userId, role, updateUrl) {
    $.ajax({
        url: updateUrl,
        type: "POST",
        data: { id: userId, role: role },
        success: function(response) {
            if (response.success) {
                showNotification("Role updated successfully", "success");
            } else {
                showNotification("Failed to update role", "error");
            }
        },
        error: function() {
            showNotification("An error occurred while updating role", "error");
        }
    });
}

// Funcție pentru afișare notificări
function showNotification(message, type) {
    // Verificăm dacă există deja un container pentru notificări
    var container = document.getElementById("notification-container");

    if (!container) {
        container = document.createElement("div");
        container.id = "notification-container";
        container.style.position = "fixed";
        container.style.top = "20px";
        container.style.right = "20px";
        container.style.zIndex = "9999";
        document.body.appendChild(container);
    }

    // Creăm notificarea
    var notification = document.createElement("div");
    notification.className = "notification " + type;
    notification.innerHTML = message;
    notification.style.backgroundColor = type === "success" ? "#4CAF50" : "#f44336";
    notification.style.color = "white";
    notification.style.padding = "15px";
    notification.style.marginBottom = "10px";
    notification.style.borderRadius = "4px";
    notification.style.boxShadow = "0 2px 5px rgba(0, 0, 0, 0.2)";
    notification.style.minWidth = "250px";

    // Adăugăm notificarea în container
    container.appendChild(notification);

    // Eliminăm notificarea după 3 secunde
    setTimeout(function() {
        notification.style.opacity = "0";
        notification.style.transition = "opacity 0.5s";

        setTimeout(function() {
            container.removeChild(notification);
        }, 500);
    }, 3000);
}

// Validare formular pentru adăugare/editare produs
function validateProductForm() {
    var name = document.getElementById("Name").value;
    var price = document.getElementById("Price").value;
    var artistName = document.getElementById("ArtistName").value;

    if (!name || name.trim() === "") {
        showNotification("Song name is required", "error");
        return false;
    }

    if (!price || isNaN(price) || parseFloat(price) < 0) {
        showNotification("Price must be a valid number greater than or equal to 0", "error");
        return false;
    }

    if (!artistName || artistName.trim() === "") {
        showNotification("Artist name is required", "error");
        return false;
    }

    return true;
}

// Validare formular pentru adăugare/editare utilizator
function validateUserForm() {
    var name = document.getElementById("Name").value;
    var email = document.getElementById("Email").value;
    var password = document.getElementById("PasswordHash").value;
    var role = document.getElementById("UserRole").value;

    if (!name || name.trim() === "") {
        showNotification("Name is required", "error");
        return false;
    }

    if (!email || !isValidEmail(email)) {
        showNotification("Valid email is required", "error");
        return false;
    }

    // Verifică dacă este un formular de adăugare sau de editare
    var isAddForm = !document.getElementById("Id") || !document.getElementById("Id").value;

    if (isAddForm && (!password || password.length < 6)) {
        showNotification("Password must be at least 6 characters long", "error");
        return false;
    }

    if (!role) {
        showNotification("Role is required", "error");
        return false;
    }

    return true;
}

// Validare email
function isValidEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

// Inițializare evenimente după încărcarea documentului
$(document).ready(function() {
    // Event listener pentru butoanele de ștergere
    $(".delete-btn").click(function() {
        var id = $(this).data("id");
        var entityType = $(this).data("type") || "item";

        confirmDelete("Are you sure you want to delete this " + entityType + "?", function() {
            // Efectuăm request-ul de ștergere
            var form = $("<form>")
                .attr("method", "post")
                .attr("action", $(this).data("url") || "")
                .append($("<input>")
                    .attr("type", "hidden")
                    .attr("name", "id")
                    .attr("value", id));

            $("body").append(form);
            form.submit();
        });
    });

    // Event listener pentru modificările de preț
    $(".price-input").change(function() {
        var id = $(this).closest("tr").data("id");
        var price = $(this).val();
        var updateUrl = $(this).data("url") || "/Admin/UpdateProductPrice";

        updateProductPrice(id, price, updateUrl);
    });

    // Event listener pentru modificările de rol
    $(".role-select").change(function() {
        var id = $(this).data("id");
        var role = $(this).val();
        var updateUrl = $(this).data("url") || "/Admin/UpdateUserRole";

        updateUserRole(id, role, updateUrl);
    });

    // Validare formulare la submit
    $("#productForm").submit(function(e) {
        if (!validateProductForm()) {
            e.preventDefault();
        }
    });

    $("#userForm").submit(function(e) {
        if (!validateUserForm()) {
            e.preventDefault();
        }
    });
});