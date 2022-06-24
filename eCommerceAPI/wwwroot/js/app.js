"use strict";
function searchProducts(searchTerm) {
  $.ajax({
    type: "post",
    url: `https://localhost:7141/api/Products/search/${searchTerm}`,
    success: function (response) {
      renderProducts(response.data);
    },
    error: function (error) {
      console.log(error.status);
    },
  });
}
function navigateTo(url) {
  let pathToProduct =
    window.location.href.replace("index.html", "") +
    "products/product.html?id=" +
    url;

  window.location.href = pathToProduct;
  console.log(pathToProduct);
}
function searchByCategory(e) {
  let value = e.target.value;
  console.log(value);
  if (value == 0) {
    startApp();
  } else {
    $.ajax({
      type: "get",
      url: `https://localhost:7141/api/Products/all/${value}`,
      success: function (response) {
        renderProducts(response.data);
      },
      error: function (error) {
        console.log(error.status);
      },
    });
  }
}
function renderProducts(products) {
  let productContainer = $("#products-container");

  $("#products-container").empty();
  products.map((product) => {
    let element = $(`
        <div class="p-1">
          <div class="card p-3" style="width: 18rem;">
              <div class="d-flex justify-content-center">
                  <div  >
                      <img src="${product.imageURL}" width="200">
                  </div>
              </div>
              <div class="text-center">
                          <h1 class="main-heading mt-0">${product.productName} </h1>
                      <div/>
                      <div class="mt-2">
                          <h5 class="text-uppercase mb-0">$${product.price}</h5>
                      </div>
              <p>${product.description}</p>
              <button class="btn btn-primary" onclick="navigateTo(${product.id})">View details</button>
          </div>
        </div>
   `);

    productContainer.append(element);
  });
}
function renderCategories(categories) {
  let categoriestContainer = $("#categoriesSelect");
  categoriestContainer.empty();
  let defaultElement = $(
    '<option value="0">--Seleccionar categoria--</option>'
  );
  categoriestContainer.append(defaultElement);
  categories.map((category) => {
    let element = $(`
     <option value="${category.id}">${category.categoryName}</option>
   `);

    categoriestContainer.append(element);
  });
}
function startApp() {
  $.ajax({
    type: "get",
    url: "https://localhost:7141/api/Categories/all",
    success: function (response) {
      renderCategories(response.data);
    },
    error: function (error) {
      console.log("====================================");
      console.log(error);
      console.log("====================================");
    },
  });

  $.ajax({
    type: "get",
    url: "https://localhost:7141/api/Products/all",
    success: function (response) {
      renderProducts(response.data);
    },
    error: function (error) {
      console.log("====================================");
      console.log(error);
      console.log("====================================");
    },
  });
}
$(document).ready(function () {
  startApp();

  let user = localStorage.getItem("user");
  console.log(user);

  // if (user == null) {
  //   alert("No session found, redirecting to login!");
  //   window.location.href = "auth/login.html";
  // } else {
  // }

  $("#searchInput").on("keyup", function () {
    var value = $(this).val().toLowerCase();

    if (value != "") {
      searchProducts(value);
    } else {
      startApp();
    }
  });
});
