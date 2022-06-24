let navbarContainer = $("#navbar-container");

function renderLoggedNavbar() {
  let navbar = $(`
    <nav class="navbar navbar-expand-lg bg-light">
        <div class="container-fluid">
          <a class="navbar-brand" href="https://localhost:7141/index.html">eCommerce</a>
          <button
            class="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarTogglerDemo02"
            aria-controls="navbarTogglerDemo02"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarTogglerDemo02">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">

            <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            Products
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown">
            <a class="dropdown-item" href="https://localhost:7141/products/list.html">List</a>

          </div>
        </li>
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            Categories
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown">
            <a class="dropdown-item" href="https://localhost:7141/categories/list.html">List</a>

          </div>
        </li>
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            Users
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown">
            <a class="dropdown-item" href="https://localhost:7141/users/list.html">List</a>

          </div>
        </li>
            </ul>
            <form class="d-flex" role="search">
              <button class="btn btn-outline-danger" type="submit" onclick="logout()">Logout</button>
            </form>
          </div>
        </div>
      </nav>
    `);

  navbarContainer.append(navbar);
}

function renderVisitorNavbar() {
  let navbar = $(`
    <nav class="navbar navbar-expand-lg bg-light">
        <div class="container-fluid">
           <a class="navbar-brand" href="https://localhost:7141/index.html">eCommerce</a>
          <button
            class="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarTogglerDemo02"
            aria-controls="navbarTogglerDemo02"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarTogglerDemo02">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">

            </ul>
            <div class="d-flex">
              <a class="btn btn-outline-success" type="submit" href="https://localhost:7141/auth/login.html">Login</a>
            </div>
          </div>
        </div>
      </nav>
    `);

  navbarContainer.append(navbar);
}

function logout() {
  localStorage.removeItem("user");
  window.location.href = "https://localhost:7141/index.html";
}

$(document).ready(function () {
  navbarContainer.empty();
  let user = localStorage.getItem("user");
  if (user == null) {
    renderVisitorNavbar();
  } else {
    renderLoggedNavbar();
  }
});
