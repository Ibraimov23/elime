
        if ($.cookie(numberKey)) {
            if ($.cookie(removeKey)) {
                if (localStorage.getItem(dishKey) != null) {
                    window.localStorage.removeItem(dishKey);
                    dishStorage = null;
                }
                if (localStorage.getItem(drinkKey) != null) {
                    window.localStorage.removeItem(drinkKey);
                    drinkStorage = null;
                }
                $.removeCookie(removeKey, { path: '/' });
                
                document.getElementById("count").innerText = basket(count);
            }
            else { 
                document.getElementById("count").innerText = basket(count);
                if (dishStorage != null) {
                    updateBtn();
                }
            }
        }
    function create(id, name, nutrit, desc, categoryId, price, image, weight) {
        if ($.cookie(numberKey)) {
            let element = {
                DishId: parseInt(id),
                Dish: {
                    Name: name,
                    Nutrit: nutrit,
                    Description: desc,
                    CategoryId: parseInt(categoryId),
                    Price: parseInt(price),
                    ImageName: image,
                    Weight: parseInt(weight),
                    Coll: parseInt(1),
                },
                NumTable: {
                    Number: parseInt(numberCookie.Number)
                }
            };
            if (dishStorage == null) {
                dishStorage = [element];
                localStorage.setItem(dishKey, JSON.stringify(dishStorage));
                update(element);
            }
            else {
                let checkId = dishStorage.find(p => p.DishId == element.DishId);
                if (checkId == null) {
                    dishStorage.push(element);
                    localStorage.setItem(dishKey, JSON.stringify(dishStorage));
                    update(element);
                }
            }
        }
    }
    function plus(id) {
        let element = dishStorage.find(({ DishId }) => DishId === id);
        element.Dish.Coll += 1;
        localStorage.setItem(dishKey, JSON.stringify(dishStorage));
        document.getElementById("number-" + element.DishId).innerText = parseInt(document.getElementById("number-" + element.DishId).innerText) + 1;
        document.getElementById("count").innerText = parseInt(document.getElementById("count").innerText) + 1;
    }
    function minus(id) {
        let element = dishStorage.find(({ DishId }) => DishId === id);
        element.Dish.Coll -= 1;
        if (element.Dish.Coll == 0) {
            dishStorage.splice(dishStorage.indexOf(element), 1);
            localStorage.setItem(dishKey, JSON.stringify(dishStorage));
            document.getElementById("created-btn-" + element.DishId).style.display = 'none';
            document.getElementById("create-btn-" + element.DishId).style.display = 'unset';
            document.getElementById("count").innerText = basket(count);
        }
        else {
            localStorage.setItem(dishKey, JSON.stringify(dishStorage));
            document.getElementById("number-" + element.DishId).innerText = parseInt(document.getElementById("number-" + element.DishId).innerText) - 1;
            document.getElementById("count").innerText = parseInt(document.getElementById("count").innerText) - 1;
        }
    }
    function remove(id) {
        const element = dishStorage.find(({ DishId }) => DishId === id);
        dishStorage.splice(dishStorage.indexOf(element), 1);
        localStorage.setItem(dishKey, JSON.stringify(dishStorage));
    }
    function filter(value) {
        $.ajax({
            url: "/",
            type: "GET",
            data: { 'category': value },
            success: function (data) {
                $('.dish-menu').replaceWith($('.dish-menu', data));
                $('.dish-foodCategories button').removeClass('selected');
                $('button.dish-criteria-' + value).addClass("selected");
                if ($.cookie(numberKey) && dishStorage != null) {
                    updateBtnCategory(value);
                }
            }
        });
    }
    function search() {
        let value = document.getElementById('search').value
        $.ajax({
            type: "POST",
            url: "/",
            data: { search: value },
            datatype: "html",
            success: function (data) {
                $('.dish-menu').replaceWith($('.dish-menu', data));
                if ($.cookie(numberKey) && dishStorage != null) {
                    updateBtnSearch(value);
                }
            }
        });
    }
    function basket(count) {
        if (dishStorage != null) {
            for (let i = 0; i < dishStorage.length; i++) {
                count += dishStorage[i].Dish.Coll;
            }
        }
        if (drinkStorage != null) {
            for (let i = 0; i < drinkStorage.length; i++) {
                count += drinkStorage[i].Drink.Coll;
            }
        }
        return count;
    }
    function update(element) {
        document.getElementById("count").innerText = basket(count);
        document.getElementById("create-btn-" + element.DishId).style.display = 'none';
        document.getElementById("created-btn-" + element.DishId).style.display = 'flex';
    }
    function updateBtnCategory(value) {
        let arr = dishStorage.filter(p => p.Dish.CategoryId == parseInt(value));
            for (let i = 0; i < arr.length; i++) {
                document.getElementById('create-btn-' + arr[i].DishId).style.display = 'none';
                document.getElementById("created-btn-" + arr[i].DishId).style.display = 'flex';
                document.getElementById("number-" + arr[i].DishId).innerText = arr[i].Dish.Coll;
            }
    }
    function updateBtnSearch(value) {
        let arr = dishStorage.filter(p => p.Dish.Name.includes(value));
            for (let i = 0; i < arr.length; i++) {
                document.getElementById('create-btn-' + arr[i].DishId).style.display = 'none';
                document.getElementById("created-btn-" + arr[i].DishId).style.display = 'flex';
                document.getElementById("number-" + arr[i].DishId).innerText = arr[i].Dish.Coll;
            }
    }
    function updateBtn() {
            for (let i = 0; i < dishStorage.length; i++) {
                document.getElementById('create-btn-' + dishStorage[i].DishId).style.display = 'none';
                document.getElementById("created-btn-" + dishStorage[i].DishId).style.display = 'flex';
                document.getElementById("number-" + dishStorage[i].DishId).innerText = dishStorage[i].Dish.Coll;
            }
    }
    function showImgLarge(imgBtn) {
        let currentImgSrc = imgBtn.parentNode.getElementsByClassName("card-img-top")[0].src;
        console.log(imgBtn.parentNode.getElementsByClassName("card-img-top")[0].src);
        document.getElementById("popUpImg").src = currentImgSrc;
        document.getElementById("popUpImgBlock").style.display = "flex";
    }
    document.getElementById("closePopUp").onclick = function () {
        this.parentNode.style.display = "none";
    };
    let timeout = null;
    document.getElementById('search').addEventListener('keyup', function (e) {
        clearTimeout(timeout);
        timeout = setTimeout(function () {
            search()
        }, 800);
    });
