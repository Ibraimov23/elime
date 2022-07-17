
    if ((dishStorage != null) ||
        (drinkStorage != null) && $.cookie(numberKey)) {
        let numberTable = JSON.parse($.cookie(numberKey));
        let arr = null;
        if (dishStorage != null) {
            arr = dishStorage.concat(drinkStorage);
        }
        else if (drinkStorage != null) {
            arr = drinkStorage.concat(dishStorage);
        }
        ReactDOM.render(
            arr.map(function (item, i) {
                if (item != null) {
                    if (item.Dish != null) {
                        price += item.Dish.Price * item.Dish.Coll;
                    }
                    else if (item.Drink != null) {                       
                        price += item.Drink.Price * item.Drink.Coll;
                    }
                }
                return html(item);
            }),
            document.getElementById("baskets")
        );
        document.getElementById("number").innerText = numberTable.Number;
        document.getElementById("coll").innerText = arr.filter(n => n).length + ' блюдо';
        document.getElementById("price").innerText = price + '₽';
    }
    else if ($.cookie(numberKey))
    {
        let numberTable = JSON.parse($.cookie(numberKey));
        document.getElementById("number").innerText = numberTable.Number;
        document.getElementById("coll").innerText = '0 блюдо';
        document.getElementById("price").innerText = '0Р';
    }
    else {
        document.getElementById("number").innerText = 0;
        document.getElementById("coll").innerText = '0 блюдо';
        document.getElementById("price").innerText = '0Р';
    }
    function plus(id, type) {
        if (type == dishKey) {
            let element = dishStorage.find(({ DishId }) => DishId === id);
            element.Dish.Coll += 1;
            localStorage.setItem(dishKey, JSON.stringify(dishStorage));
            document.getElementById("dish-count-" + element.DishId).innerText = parseInt(document.getElementById("dish-count-" + element.DishId).innerText) + 1;
            document.getElementById("price").innerText = parseInt(document.getElementById("price").innerText) + parseInt(element.Dish.Price) + '₽';
        }
        else if (type == drinkKey) {
            let element = drinkStorage.find(({ DrinkId }) => DrinkId === id);
            element.Drink.Coll += 1;
            localStorage.setItem(drinkKey, JSON.stringify(drinkStorage));
            document.getElementById("drink-count-" + element.DrinkId).innerText = parseInt(document.getElementById("drink-count-" + element.DrinkId).innerText) + 1;
            document.getElementById("price").innerText = parseInt(document.getElementById("price").innerText) + parseInt(element.Drink.Price) + '₽';
        }
    }
    function minus(id, type) {
        if (type == dishKey) {
            let element = dishStorage.find(({ DishId }) => DishId === id);
            element.Dish.Coll -= 1;
            if (element.Dish.Coll == 0) {
                remove(id, dishKey);
            } else {
                localStorage.setItem(dishKey, JSON.stringify(dishStorage));
                document.getElementById("dish-count-" + element.DishId).innerText = parseInt(document.getElementById("dish-count-" + element.DishId).innerText) - 1;
                document.getElementById("price").innerText = parseInt(document.getElementById("price").innerText) - parseInt(element.Dish.Price) + '₽';
            }
        }
        else if (type == drinkKey) {
            let element = drinkStorage.find(({ DrinkId }) => DrinkId === id);
            element.Drink.Coll -= 1;
            if (element.Drink.Coll == 0) {
                remove(id, drinkKey);
            } else {
                localStorage.setItem(drinkKey, JSON.stringify(drinkStorage));
                document.getElementById("drink-count-" + element.DrinkId).innerText = parseInt(document.getElementById("drink-count-" + element.DrinkId).innerText) - 1;
                document.getElementById("price").innerText = parseInt(document.getElementById("price").innerText) - parseInt(element.Drink.Price) + '₽';
            }
        }
    }
    function remove(id, type) {
        if (type == dishKey) {
            const element = dishStorage.find(({ DishId }) => DishId === id);
            dishStorage.splice(dishStorage.indexOf(element), 1);
            localStorage.setItem(dishKey, JSON.stringify(dishStorage));
            let el = document.getElementById("dish-basket" + id);
            if (el != null) {
                el.remove();
                document.getElementById("coll").innerText = parseInt(document.getElementById("coll").innerText) - 1 + ' блюдо';
                document.getElementById("price").innerText = parseInt(document.getElementById("price").innerText) - parseInt(element.Dish.Price) + '₽';
            }
        }
        else if (type == drinkKey) {
            const element = drinkStorage.find(({ DrinkId }) => DrinkId === id);
            drinkStorage.splice(drinkStorage.indexOf(element), 1);
            localStorage.setItem(drinkKey, JSON.stringify(drinkStorage));
            let el = document.getElementById("drink-basket" + id);
            if (el != null) {
                el.remove();
                document.getElementById("coll").innerText = parseInt(document.getElementById("coll").innerText) - 1 + ' блюдо';
                document.getElementById("price").innerText = parseInt(document.getElementById("price").innerText) - parseInt(element.Drink.Price) + '₽';
            }
        }
    }
    function showImgLarge() {
        let currentImgSrc = document.getElementsByClassName("card-img-top")[0].src;
        document.getElementById("popUpImg").src = currentImgSrc;
        document.getElementById("popUpImgBlock").style.display = "flex";
    }
    document.getElementById("closePopUp").onclick = function () {
        this.parentNode.style.display = "none";
    };
    function html(item) {
        if (item != null) {
            if (item.Dish != null) {
                return <div id={'dish-basket' + item.DishId}>
                    <div class="col-12">
                        <div class="card basket-card p-0">
                            <div class="row">
                                <div class="basket-block_img">
                                    <div class="img-wrapper">
                                        <img class="card-img-top" src={'/images/dishes/' + item.Dish.ImageName} alt="Not Image" />
                                        <img src="/images/embedded/arrow.png" onClick={() => showImgLarge()} alt="" class="pop-up-button" />
                                    </div>
                                </div>
                                <div class="basket-block_info">
                                    <div class="basket-card-body card-body">
                                        <h5 class="card-title">{item.Dish.Name}</h5>
                                    </div>
                                    <div class="basket-calc">
                                        <button type="button" class="basket-numord basket-button-color">
                                            <img class="minus" src="/images/embedded/minus.png" onClick={() => minus(item.DishId, 'dish', 'DishId')} />
                                            <span class="number" id={'dish-count-' + item.DishId}>{item.Dish.Coll}</span>
                                            <img class="plus" src="/images/embedded/plus.png" onClick={() => plus(item.DishId, 'dish')} />
                                        </button>
                                        <button type="button" class="basket-prise basket-button-color">{item.Dish.Price}₽</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr class="basket-line" />
                </div>;
            }
            else if (item.Drink != null) {
                return <div id={'drink-basket' + item.DrinkId}>
                    <div class="col-12">
                        <div class="card basket-card p-0">
                            <div class="row">
                                <div class="basket-block_img">
                                    <div class="img-wrapper">
                                        <img class="card-img-top" src={'/images/drinkes/' + item.Drink.ImageName} alt="Not Image" />
                                        <img src="/images/embedded/arrow.png" onClick={() => showImgLarge()} alt="" class="pop-up-button" />
                                    </div>
                                </div>
                                <div class="basket-block_info">
                                    <div class="basket-card-body card-body">
                                        <h5 class="card-title">{item.Drink.Name}</h5>
                                    </div>
                                    <div class="basket-calc">
                                        <button type="button" class="basket-numord basket-button-color">
                                            <img class="minus" src="/images/embedded/minus.png" onClick={() => minus(item.DrinkId, 'drink')} />
                                            <span class="number" id={'drink-count-' + item.DrinkId}>{item.Drink.Coll}</span>
                                            <img class="plus" src="/images/embedded/plus.png" onClick={() => plus(item.DrinkId, 'drink')} />
                                        </button>
                                        <button type="button" class="basket-prise basket-button-color">{item.Drink.Price}₽</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr class="basket-line" />
                </div>;
            }
        }
    }

