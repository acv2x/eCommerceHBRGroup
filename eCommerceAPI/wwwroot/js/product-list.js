"use strict";
let selectedProduct = {
  id: 0,
  productName: "",
  price: "",
  description: "",
  categoryID: 0,
  imageURL: "",
};

let action = "new";

function functionOpenEditModal(
  id,
  productName,
  price,
  description,
  categoryID,
  imageURL,
  actionGot
) {
  action = actionGot;

  if (action.toLowerCase() === "edit") {
    selectedProduct.id = id;
    selectedProduct.productName = productName;
    selectedProduct.price = price;
    selectedProduct.description = description;
    selectedProduct.categoryID = categoryID;
    selectedProduct.imageURL = imageURL;

    $("#productName").val(productName);
    $("#productPrice").val(price);
    $("#productDescription").val(description);
    $("#categoryID").val(categoryID);
    $("#imageURL").val(imageURL);
  } else {
    $("#userproductNamename").val();
    $("#productPrice").val();
    $("#productDescription").val();
    $("#categoryID").val(1);
    $("#imageURL").val();
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
          url: `/api/products/delete/${id}`,
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

function renderCategories(categories) {
  let categoryContainer = $("#categoryID");

  categories.map((category) => {
    categoryContainer.append(
      `<option value="${category.id}">${category.categoryName}</option>`
    );
  });
}
function renderProducts(products) {
  let productContainer = $("body > div.container > table > tbody");

  products.map((product) => {
    let element = $(`
        <tr>
            <th scope="row">${product.id}</th>
            <td>${product.productName}</td>
            <td>${product.price}</td>
            <td>
            <button
             type="button"
              class="btn btn-warning"
              data-toggle="modal"
              data-target="#editProductModal"
             onclick="functionOpenEditModal(${product.id},'${product.productName}','${product.price}','${product.description}',${product.categoryID},'${product.imageURL}','edit')" type="submit">
                <i class="fa-solid fa-pen-to-square"></i>
            </button>
            <button class="btn btn-danger" type="submit" onclick="deleteElement(${product.id})">
                <i class="fa-solid fa-trash"></i>
            </button>
            </td>
        </tr>
   `);

    productContainer.append(element);
  });
}

function saveUserOrChanges() {
  selectedProduct.productName = $("#productName").val();
  selectedProduct.price = $("#productPrice").val();
  selectedProduct.description = $("#productDescription").val();
  selectedProduct.categoryID = $("#categoryID").val();
  selectedProduct.imageURL = $("#imageURL").val();

  if (action != "new") {
    $.ajax({
      type: "POST",
      url: `https://localhost:7141/api/Products/edit/${selectedProduct.id}`,
      data: JSON.stringify(selectedProduct),
      contentType: "application/json",
      success: function () {
        location.reload();
      },
    });
  } else {
    $.ajax({
      type: "POST",
      url: "https://localhost:7141/api/Products/create",
      data: JSON.stringify(selectedProduct),
      contentType: "application/json",
      success: function () {
        location.reload();
      },
    });
  }
}

$(document).ready(function () {
  $("#categoryForm").validate({
    rules: {
      productName: {
        required: true,
        minlength: 4,
      },
      categoryID: {
        required: true,
      },
      productPrice: {
        required: true,
      },
      productDescription: {
        required: true,
      },
      imageURL: {
        required: true,
      },
    },
  });

  $.ajax({
    type: "get",
    url: `https://localhost:7141/api/Products/all`,
    success: function (response) {
      renderProducts(response.data);
    },
    error: function (error) {
      console.log("====================================");
      console.log(error);
      console.log("====================================");
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
