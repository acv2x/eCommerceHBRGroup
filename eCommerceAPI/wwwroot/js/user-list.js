"use strict";
let selectedUser = {
  id: 0,
  fullName: "",
  email: "",
  password: "",
};
let action = "new";
function functionOpenEditModal(id, fullName, email, password, actionGot) {
  action = actionGot;

  setTimeout(() => {
    console.log("Updatin variables");
  }, 1000)
  if (action.toLowerCase() === "edit") {
    selectedUser.id = id;
    selectedUser.fullName = fullName;
    selectedUser.email = email;
    selectedUser.password = password;

    $("#username").val(fullName);
    $("#email").val(email);
    $("#password").val(password);
  } else {
    $("#username").val("");
    $("#email").val("");
    $("#password").val("");
  }
}

function deleteElement(id) {
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: "btn btn-success",
      cancelButton: "btn btn-danger",
    },
    buttonsStyling: false,
  });

  swalWithBootstrapButtons
    .fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes, delete it!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true,
    })
    .then((result) => {
      if (result.isConfirmed) {
        $.ajax({
          type: "GET",
          url: `https://localhost:7141/api/users/delete/${id}`,
          success: function () {
            swalWithBootstrapButtons.fire(
              "Deleted!",
              "Your file has been deleted.",
              "success"
            );
            location.reload();
          },
        });
      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire("Cancelled", "Action canceled", "error");
      }
    });
}
function saveUserOrChanges() {
  selectedUser.fullName = $("#username").val();
  selectedUser.email = $("#email").val();
  selectedUser.password = $("#password").val();

  if ( action != "new") {
    debugger;

    $.ajax({
      type: "POST",
      url: "https://localhost:7141/api/Users/update",
      data: JSON.stringify(selectedUser),
      contentType: "application/json",
      success: function () {
        location.reload();
      },
    });
  } else {
    $.ajax({
      type: "POST",
      url: "https://localhost:7141/api/Users/signup",
      data: JSON.stringify(selectedUser),
      contentType: "application/json",
      success: function () {
        location.reload();
      },
    });
  }
}
function renderUsers(users) {
  let userContainer = $("body > div.container > table > tbody");

  users.map((user) => {
    let element = $(`
        <tr>
            <th scope="row">${user.id}</th>
            <td>${user.fullName}</td>
            <td>${user.email}</td>
            <td>
            <button
              type="button"
              class="btn btn-warning"
              data-toggle="modal"
              data-target="#editUserModal"
              onclick="functionOpenEditModal(${user.id}, '${user.fullName}', '${user.email}', '${user.password}', 'edit')"
            >
               <i class="fa-solid fa-pen-to-square"></i>
            </button>

            <button class="btn btn-danger" type="submit" data-id="${user.id}" id="deleteButton-${user.id}" onclick="deleteElement(${user.id})">
                <i class="fa-solid fa-trash"></i>
            </button>
            </td>
        </tr>
    `);

    userContainer.append(element);
  });
}
$(document).ready(function () {
  $("#userForm").validate({
    rules: {
      username: {
        required: true,
        minlength: 4,
      },
      email: {
        required: true,
        email: true,
      },
      password: {
        required: true,
        minlength: 8,
        maxlength: 24,
      },
    },
  });
  $.ajax({
    type: "GET",
    url: `https://localhost:7141/api/Users/Get/all`,
    success: function (response) {
      renderUsers(response.data);
    },
    error: function (error) {
      console.log("====================================");
      console.log(error);
      console.log("====================================");
    },
  });
});
