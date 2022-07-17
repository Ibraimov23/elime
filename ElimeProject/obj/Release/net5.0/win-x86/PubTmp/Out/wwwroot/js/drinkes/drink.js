
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
                if (drinkStorage != null) {
                    updateBtn();
                }
            }
        }
    function create(id, name, nutrit, desc, categoryId, price, image, mill) {
        if ($.cookie(numberKey)) {
            let element = {
                DrinkId: parseInt(id),
                Drink: {
                    Name: name,
                    Nutrit: nutrit,
                    Description: desc,
                    CategoryId: parseInt(categoryId),
                    Price: parseInt(price),
                    ImageName: image,
                    Millit: parseInt(mill),
                    Coll: parseInt(1),
                },
                NumTable: {
                    Number: parseInt(numberCookie.Number)
                }
            };
            if (drinkStorage == null) {
                drinkStorage = [element];
                localStorage.setItem(drinkKey, JSON.stringify(drinkStorage));
                update(element);
            }
            else {
                let checkId = drinkStorage.find(p => p.DrinkId == element.DrinkId);
                if (checkId == null) {
                    drinkStorage.push(element);
                    localStorage.setItem(drinkKey, JSON.stringify(drinkStorage));
                    update(element);
                }
            }
        }
    }
    function plus(id) {
        let element = drinkStorage.find(({ DrinkId }) => DrinkId === id);
        element.Drink.Coll += 1;
        localStorage.setItem(drinkKey, JSON.stringify(drinkStorage));
        document.getElementById("number-" + element.DrinkId).innerText = parseInt(document.getElementById("number-" + element.DrinkId).innerText) + 1;
        document.getElementById("count").innerText = parseInt(document.getElementById("count").innerText) + 1;
    }
    function minus(id) {
        let element = drinkStorage.find(({ DrinkId }) => DrinkId === id);
        element.Drink.Coll -= 1;
        if (element.Drink.Coll == 0) {
            drinkStorage.splice(drinkStorage.indexOf(element), 1);
            localStorage.setItem(drinkKey, JSON.stringify(drinkStorage));
            document.getElementById("created-btn-" + element.DrinkId).style.display = 'none';
            document.getElementById("create-btn-" + element.DrinkId).style.display = 'unset';
            document.getElementById("count").innerText = basket(count);
        }
        else {
            localStorage.setItem(drinkKey, JSON.stringify(drinkStorage));
            document.getElementById("number-" + element.DrinkId).innerText = parseInt(document.getElementById("number-" + element.DrinkId).innerText) - 1;
            document.getElementById("count").innerText = parseInt(document.getElementById("count").innerText) - 1;
        }
    }
    function remove(id) {
        const element = drinkStorage.find(({ DrinkId }) => DrinkId === id);
        drinkStorage.splice(drinkStorage.indexOf(element), 1);
        localStorage.setItem(drinkKey, JSON.stringify(drinkStorage));
    }
    function filter(value) {
        $.ajax({
            url: "/drinkes",
            type: "GET",
            data: { 'category': value },
            success: function (data) {
                $('.drink-menu').replaceWith($('.drink-menu', data));
                $('.drink-Categories button').removeClass('selected');
                $('button.drink-criteria-' + value).addClass("selected");
                if ($.cookie(numberKey) && drinkStorage != null) {
                    updateBtnCategory(value);
                }
            }
        });
    }
    function search() {
        let value = document.getElementById('search').value
        $.ajax({
            type: "POST",
            url: "/drinkes",
            data: { search: value },
            datatype: "html",
            success: function (data) {
                $('.drink-menu').replaceWith($('.drink-menu', data));
                if ($.cookie(numberKey) && drinkStorage != null) {
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
        document.getElementById("create-btn-" + element.DrinkId).style.display = 'none';
        document.getElementById("created-btn-" + element.DrinkId).style.display = 'flex';
    }
    function updateBtnCategory(value) {
        let arr = drinkStorage.filter(p => p.Drink.CategoryId == parseInt(value));
            for (let i = 0; i < arr.length; i++) {
                document.getElementById('create-btn-' + arr[i].DrinkId).style.display = 'none';
                document.getElementById("created-btn-" + arr[i].DrinkId).style.display = 'flex';
                document.getElementById("number-" + arr[i].DrinkId).innerText = arr[i].Drink.Coll;
        }
    }
    function updateBtnSearch(value) {
        let arr = drinkStorage.filter(p => p.Drink.Name.includes(value));
            for (let i = 0; i < arr.length; i++) {
                document.getElementById('create-btn-' + arr[i].DrinkId).style.display = 'none';
                document.getElementById("created-btn-" + arr[i].DrinkId).style.display = 'flex';
                document.getElementById("number-" + arr[i].DrinkId).innerText = arr[i].Drink.Coll;
        }
    }
    function updateBtn() {
        for (let i = 0; i < drinkStorage.length; i++) {
            document.getElementById('create-btn-' + drinkStorage[i].DrinkId).style.display = 'none';
            document.getElementById("created-btn-" + drinkStorage[i].DrinkId).style.display = 'flex';
            document.getElementById("number-" + drinkStorage[i].DrinkId).innerText = drinkStorage[i].Drink.Coll;
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
