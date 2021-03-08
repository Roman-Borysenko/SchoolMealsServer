use SchoolMeals;

delete from [dbo].[Tags] DBCC CHECKIDENT ('SchoolMeals.dbo.Tags',RESEED, 0);
delete from [dbo].[Ingredients] DBCC CHECKIDENT ('SchoolMeals.dbo.Ingredients',RESEED, 0);
delete from [dbo].[Dishes] DBCC CHECKIDENT ('SchoolMeals.dbo.Dishes',RESEED, 0);
delete from [dbo].[DishTag] DBCC CHECKIDENT ('SchoolMeals.dbo.DishTag',RESEED, 0);
delete from [dbo].[DishIngredients] DBCC CHECKIDENT ('SchoolMeals.dbo.DishIngredients',RESEED, 0);
delete from [dbo].[Categories] DBCC CHECKIDENT ('SchoolMeals.dbo.Categories',RESEED, 0);
delete from [dbo].[Languages] DBCC CHECKIDENT ('SchoolMeals.dbo.Languages',RESEED, 0);

insert into [dbo].[Languages] ([Name], [NameAbbreviation], [Slug]) values 
(N'Українська', 'UA', 'ukrainian'),
(N'Русский', 'RU', 'russian'),
('English', 'EN', 'english');

insert into [dbo].[Tags] ([Name], [Slug], [LanguageId]) values
(N'Вегетаріанські', 'vegetarian', 1),
(N'Сніданок', 'breakfast', 1),

(N'Вегетарианские', 'vegetarian', 2),
(N'Завтрак', 'breakfast', 2),

('Vegetarian', 'vegetarian', 3),
('Breakfast', 'breakfast', 3);

insert into [dbo].[Ingredients] ([Name], [Slug], [Calories], [LanguageId]) values
(N'Картопля', 'potato', 100, 1),
(N'Помідори', 'tomatoes', 100, 1),
(N'Огірки', 'cucumbers', 100, 1),
(N'Капуста', 'cabbage', 100, 1),
(N'Морква', 'carrot', 100, 1),
(N'Цибуля', 'onion', 100, 1),

(N'Картофель', 'potato', 100, 2),
(N'Помидоры', 'tomatoes', 100, 2),
(N'Огурцы', 'cucumbers', 100, 2),
(N'Капуста', 'cabbage', 100, 2),
(N'Морковь', 'carrot', 100, 2),
(N'Лук', 'onion', 100, 2),

('Potato', 'potato', 100, 3),
('Tomatoes', 'tomatoes', 100, 3),
('Cucumbers', 'cucumbers', 100, 3),
('Cabbage', 'cabbage', 100, 3),
('Carrot', 'carrot', 100, 3),
('Onion', 'onion', 100, 3);

insert into [dbo].[Categories] ([Name], [Slug], [CategoryId], [LanguageId]) values
(N'Супи', 'soups', null, 1),
(N'Звичайні супи', 'ordinary-soups', 1, 1),
(N'Крем-супи', 'cream-soups', 1, 1),
(N'Азіатські супи', 'asian-soups', 1, 1),
(N'Салати', 'salads', null, 1),
(N'Основні страви', 'main-dishes', null, 1),
(N'Десерти', 'desserts', null, 1),

(N'Супы', 'soups', null, 2),
(N'Обычные супы', 'ordinary-soups', 8, 2),
(N'Крем-супы', 'cream-soups', 8, 2),
(N'Азиатские супы', 'asian-soups', 8, 2),
(N'Салаты', 'salads', null, 2),
(N'Основные блюда', 'main-dishes', null, 2),
(N'Десерты', 'desserts', null, 2),

('Soups', 'soups', null, 3),
('Ordinary soups', 'ordinary-soups', 15, 3),
('Cream soups', 'cream-soups', 15, 3),
('Asian soups', 'asian-soups', 15, 3),
('Salads', 'salads', null, 3),
('Main dishes', 'main-dishes', null, 3),
('Desserts', 'desserts', null, 3);

insert into [dbo].[Dishes] ([Name], [Slug], [Description], [Image], [IsRecommend], [CategoryId], [LanguageId]) values 
(N'Гречаний суп', 'buckwheat-soup', N'Суп з гречаної крупи - традиційне блюдо української та білоруської кухні, що представляє собою суп, приготований на мясному бульйоні або на бульйоні з кісток, яловичих або свинячих, з додаванням гречаної крупи.', '8.jpg', 1, 2, 1),
(N'Грибний крем-суп', 'mushroom-cream-soup', N'Грибний крем суп з печериць, як втім і всі інші крем супи традиційно вважаються стравами-представниками європейської кухні. Вони, як правило, в основі своїй мають молочні вершки, молоко або плавлені сирки. Ці інгредієнти надають такого роду страв приємну ніжну текстуру і наділяють їх витонченим смаком!
Що ж стосується грибного крем супу, то він у своїй основі містить молоко. Друга основна складова цієї страви - це відповідно гриби. Також сюди ще входять цибуля, часник і спеції. Хоча список інгредієнтів необхідних для приготування цієї страви і невеликий, але все ж його смакові якості від цього анітрохи не зменшуються.
Приєднуйтесь до нас в приготуванні грибного крем супу і поповните свою колекцію фото рецептів приготування перших страв!', '8.jpg', 1, 3, 1),
(N'Суп удон', 'udon-soup', N'Локшина удон це оригінальні макаронні вироби нарізної виду, вироблені на основі пшеничного борошна. Продукт користується широким попитом у жителів Азіатських країн, але останнім часом впевнено займає свої позиції і на вітчизняному ринку.', '8.jpg', 1, 4, 1),
(N'Місо суп', 'miso-soup', N'Одне з національних надбань Японії - це місо суп. Є різні його варіації, залежить від регіону: з креветками, грибами, вегетаріанський. Записуйте рецепт, як приготувати місо суп класичний.', '8.jpg', 1, 4, 1),
(N'Цезар з куркою', 'caesar-with-chicken', N'Салат «Цезар» - популярний салат, одне з найвідоміших страв північноамериканської кухні. У класичній версії основними складовими салату є пшеничні крутон (грінки), листя салату-ромен і тертий пармезан, заправлені соусом. Основа заправки «Цезар» - свіжі яйця, витримані 1 хвилину в окропі і охолоджені (або яйця в сорочках). Далі яйця збивають з оливковою олією і приправляють часником, лимонним соком і Вустерского соусом. У класичному вигляді салат виходить досить легким, тому до нього часто додають поживні складові, наприклад, круте яйце або смажену курку.', '8.jpg', 1, 5, 1),

(N'Гречневый суп', 'buckwheat-soup', N'Суп из гречневой крупы - традиционное блюдо украинской и белорусской кухни, представляет собой суп, приготовленный на мясном бульоне или на бульоне из костей, говяжьих или свиных, с добавлением гречневой крупы.', '8.jpg', 1, 9, 2),
(N'Грибной крем-суп', 'mushroom-cream-soup', N'Грибной крем суп из шампиньонов, как впрочем и все остальные крем супы традиционно считаются блюдами-представителями европейской кухни. Они, как правило, в основе своей имеют молочные сливки, молоко или плавленые сырки. Эти ингредиенты придают такого рода блюд приятную нежную текстуру и наделяют их изысканным вкусом!
Что же касается грибного крем супа, то он в своей основе содержит молоко. Вторая основная составляющая этого блюда - это соответственно грибы. Также сюда еще входят лук, чеснок и специи. Хотя список ингредиентов необходимых для приготовления этого блюда и небольшой, но все же его вкусовые качества от этого нисколько не уменьшаются.
Присоединяйтесь к нам в приготовлении грибного крем супа и пополните свою коллекцию фото рецептов приготовления первых блюд!', '8.jpg', 1, 10, 2),
(N'Суп удон', 'udon-soup', N'Лапша удон это оригинальные макаронные изделия нарезного вида, произведенные на основе пшеничной муки. Продукт пользуется широким спросом у жителей Азиатских стран, но в последнее время уверенно занимает свои позиции и на отечественном рынке.', '8.jpg', 1, 11, 2),
(N'Мисо суп', 'miso-soup', N'Одно из национальных достояний Японии - это мисо суп. Есть разные его вариации, зависит от региона: с креветками, грибами, вегетарианский. Записывайте рецепт, как приготовить мисо суп классический.', '8.jpg', 1, 11, 2),
(N'Цезарь с курицей', 'caesar-with-chicken', N'Салат «Цезарь» - популярный салат, одно из самых известных блюд североамериканской кухни. В классической версии основными составляющими салата являются пшеничные крутоны (гренки), листья салата-ромэн и тертый пармезан, заправленные соусом. Основа заправки «Цезарь» - свежие яйца, выдержанные 1 минуту в кипятке и охлажденные (или яйца в рубашках). Далее яйца взбивают с оливковым маслом и приправляют чесноком, лимонным соком и Вустерского соусом. В классическом виде салат получается довольно легким, поэтому к нему часто добавляют питательные составляющие, например, крутое яйцо или жареную курицу.', '8.jpg', 1, 12, 2),

('Buckwheat soup', 'buckwheat-soup', 'Buckwheat soup - a traditional dish of Ukrainian and Belarusian cuisine, which is a soup made from meat broth or broth from bones, beef or pork, with the addition of buckwheat.', '8.jpg', 1, 16, 3),
('Mushroom cream soup', 'mushroom-cream-soup', 'Mushroom cream mushroom soup, like all other cream soups are traditionally considered to be dishes of European cuisine. They are usually based on milk cream, milk or processed cheese. These ingredients give this kind of dishes a nice delicate texture and give them a delicate taste!
As for the mushroom cream soup, it basically contains milk. The second main component of this dish is mushrooms, respectively. It also includes onions, garlic and spices. Although the list of ingredients needed for cooking this dish is small, but still its taste does not decrease at all.
Join us in the preparation of mushroom cream soup and add to your collection of photos of recipes for cooking first courses!', '8.jpg', 1, 17, 3),
('Udon soup', 'udon-soup', 'Udon noodles are original sliced pasta made from wheat flour. The product is in great demand among residents of Asian countries, but recently confidently takes its place in the domestic market.', '8.jpg', 1, 18, 3),
('Miso soup', 'miso-soup', 'One of the national treasures of Japan is miso soup. There are different variations, depending on the region: with shrimp, mushrooms, vegetarian. Write down the recipe for how to cook miso classic soup.', '8.jpg', 1, 18, 3),
('Caesar with chicken', 'caesar-with-chicken', 'Caesar salad is a popular salad, one of the most famous dishes of North American cuisine. In the classic version, the main ingredients of the salad are wheat croutons (croutons), romaine lettuce leaves and grated parmesan, dressed with sauce. The basis of the Caesar dressing is fresh eggs, kept in boiling water for 1 minute and chilled (or eggs in shirts). Next, the eggs are beaten with olive oil and seasoned with garlic, lemon juice and Worcester sauce. In the classic form, the salad turns out to be quite light, so nutritious components, for example, a hard egg or fried chicken, are often added to it.', '8.jpg', 1, 19, 3);

insert into [dbo].[DishTag] ([DishId], [TagId]) values
(1, 1),
(1, 2),
(2, 1),
(3, 2),

(6, 3),
(6, 4),
(7, 3),
(8, 4),

(11, 5),
(11, 6),
(12, 5),
(13, 6);

insert into [dbo].[DishIngredients] ([DishId], [IngredientId]) values
(1, 1),
(1, 5),
(1, 6),
(5, 4),

(6, 7),
(6, 11),
(6, 12),
(10, 10),

(11, 13),
(11, 17),
(11, 18),
(15, 16);