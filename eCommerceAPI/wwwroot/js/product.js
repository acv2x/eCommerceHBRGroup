"use strict";

function renderProducts(product) {
  let productContainer = $("#product-container");

  let element = $(`
  <div class="row m-0">
        <div class="col-lg-4 left-side-product-box pb-3">
          <img
            src="${product.imageURL}"
            class="border p-3"
            style="width: 350px; height: 350px"
          />
        </div>
        <div class="col-lg-8">
          <div class="right-side-pro-detail border p-3 m-0">
            <div class="row">
            <div class="col-lg-12">
                <h5>Product Price</h5>
                <h4>${product.productName}</h4>
                <hr class="p-0 m-0" />
              </div>
              <div class="col-lg-12">
                <h5>Product Price</h5>
                <p class="m-0 p-0 price-pro">$${product.price}</p>
                <hr class="p-0 m-0" />
              </div>
              <div class="col-lg-12 pt-2">
                <h5>Product Detail</h5>
                <span
                  >${product.description}</span
                >
                <hr class="m-0 pt-2 mt-2" />
              </div>

              <div class="col-lg-12">
                <h6>Quantity :</h6>
                <input
                  type="number"
                  class="form-control text-center w-100"
                  value="1"
                />
              </div>
              <div class="col-lg-12 mt-3">
                <div class="row">
                  <div class="col-lg-6 pb-2">
                    <a href="#" class="btn btn-danger w-100">Add To Cart</a>
                  </div>
                  <div class="col-lg-6">
                    <a href="#" class="btn btn-success w-100">Shop Now</a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
  `);
  productContainer.append(element);
}

function productInit() {
  console.log("Product started");
  const urlParams = new URLSearchParams(window.location.search);
  const productID = urlParams.get("id");

  $.ajax({
    type: "get",
    url: `https://localhost:7141/api/Products/${productID}`,
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
  productInit();
});
