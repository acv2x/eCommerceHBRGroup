"use strict";
let selectedCategory = {
  id: 0,
  categoryName: "",
};
let action = "new";
function functionOpenEditModal(id, categoryName, actionGot) {
  action = actionGot;

  if (action.toLowerCase() === "edit") {
    selectedCategory.id = id;
    selectedCategory.categoryName = categoryName;
    $("#categoryName").val(categoryName);
  } else {
    $("#categoryName").val("");
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
          type: "POST",
          url: `https://localhost:7141/api/Categories/delete/${id}`,
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
  selectedCategory.categoryName = $("#categoryName").val();

  if ($("#userForm").valid() && action != "new") {
    debugger;

    $.ajax({
      type: "POST",
      url: `https://localhost:7141/api/Categories/edit/${selectedCategory.id}`,
      data: JSON.stringify(selectedCategory),
      contentType: "application/json",
      success: function () {
        location.reload();
      },
    });
  } else {
    $.ajax({
      type: "POST",
      url: `https://localhost:7141/api/Categories/create`,
      data: JSON.stringify(selectedCategory),
      contentType: "application/json",
      success: function () {
        location.reload();
      },
    });
  }
}
function renderCategories(categories) {
  let categoriesContainer = $("body > div.container > table > tbody");

  categories.map((category) => {
    let element = $(`
        <tr>
            <th scope="row">${category.id}</th>
            <td>${category.categoryName}</td>
            <td>
            <button
             type="button"
              class="btn btn-warning"
              data-toggle="modal"
              data-target="#editCategoryModal"
             onclick="functionOpenEditModal(${category.id}, '${category.categoryName}', 'edit')" type="submit">
                <i class="fa-solid fa-pen-to-square"></i>
            </button>
            <button class="btn btn-danger" type="submit" id="deleteButton-${category.id}" onclick="deleteElement(${category.id})">
                <i class="fa-solid fa-trash"></i>
            </button>
            </td>
        </tr>
    `);

    categoriesContainer.append(element);
  });
}
$(document).ready(function () {
  $("#userForm").validate({
    rules: {
      categoryName: {
        required: true,
        minlength: 4,
      },
    },
  });

  $.ajax({
    type: "get",
    url: `https://localhost:7141/api/Categories/all`,
    success: function (response) {
      renderCategories(response.data);
    },
    error: function (error) {
      console.log("====================================");
      console.log(error);
      console.log("====================================");
    },
  });
});
