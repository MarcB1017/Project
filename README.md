CAHIER DES CHARGES


But :
	Dans ce projet personnel, j’ai décidé de mettre en pratique mes compétences acquises lors de mon parcours collégial. J’ai réalisé ce projet avec Visual Studio 2022 en utilisant le patron de conception modèle, vue et contrôleur (MVC/MVVM) en Asp.Net C#, à l’aide des technologies suivantes : EntityFrameworkCore, Identity, SqlExpress, SqlServer, Cloudinary, Bootstrap.

Mise en situation :
La compagnie « CarsForSale » a besoin d’un système leur permettant de vendre des voitures, des motos et des mini-fourgonnettes en ligne. Cette compagnie veut permettre à ceux qui désirent de placer une annonce en ligne pour les membres. Ils veulent aussi permettre aux utilisateurs de consulter les annonces. Ils ont aussi besoin d’un administrateur qui sera en mesure de gérer les membres et les annonces de la plateforme.

Spécifications applicatives :
Puisqu’il devra être utilisé à la fois par un administrateur, des utilisateurs et des membres, le système devra offrir des fonctionnalités administratives, des fonctionnalités pour les utilisateurs et des fonctionnalités pour les membres. 

1)	Fonctionnalités administratives.
1-	Un administrateur a tous les droits. Il peut supprimer n’importe quelle annonce ou     utilisateur de la plateforme.

2)	Fonctionnalités pour les utilisateurs.
1-   Peut consulter les annonces sans être membre.
2-	Ne peux pas accéder à la page admin et au tableau de bord.
3-	Ne peut pas vendre ou modifier une annonce. 
	
3)   Fonctionnalités pour les membres. 
1-	Doit être membre pour accéder au tableau de bord.
2-	Doit compléter son profil pour accéder au tableau de bord complet.
3-	Doit être membre pour vendre, modifier ou supprimer une annonce.
4-	Un membre peut seulement modifier ou supprimer les annonces qui lui appartient.
5-	Doit utiliser un mot de passe fort. (Longueur de 8 caractères minimum, incluant Maj et caractères spéciaux [!@#$%].
6-	Doit utiliser une adresse courriel valide contenant un @ et un point (.) suivi de minimum 2 lettres.
7-	Seulement une inscription est permise par adresse courriel. 

4)	Fonctionnalités globales :

1-	Afficher les voitures disponibles à proximité du lieu de la personne connectée sur la plateforme.


5) Structure minimale de l’application :

Modèle (Classes) :
1-	AppUser, contenant les champs Id, prénom, nom de famille, numéro de téléphone, photo, FK Address, Liste/Collection de voitures, motos et mini-fourgonnettes.
2-	Address, contenant les champs pays, ville et province, FK de AppUser.
3-	Bikes contenant les champs Id, prix, kilométrage, marque, modèle, année, description et image.
4-	Cars, contenant les champs Id, prix, kilométrage, marque, modèle, année, description et image.
5-	Vans, contenant les champs Id, prix, kilométrage, marque, modèle, année, description et image.

Vues :
Home/Index, Page d’accueil.
Account/Register, Permet d’afficher la page d’inscription.
Account/Login, Permet d’afficher la page pour se connecter une fois inscrit.
Cars/Index, Permet d’afficher page d’accueil des voitures disponibles.
Car/Create, permet d’afficher la page pour vendre sa voiture.
Bikes/Index, Permet d’afficher page d’accueil des motos disponibles.
Bikes/Create, permet d’afficher la page pour vendre sa moto.
Vans/Index, Permet d’afficher page d’accueil des mini-fourgonnettes disponibles.
Vans/Create, permet d’afficher la page pour vendre sa mini-fourgonnette.
DashBoard/Index, permet d’afficher la page des membres.
DashBoard/EditCar, permet d’afficher la page pour modifier son annonce.
DashBoard/EditVan, permet d’afficher la page pour modifier son annonce.
DashBoard/EditBike, permet d’afficher la page pour modifier son annonce.
DashBoard/ViewAll, permet d’afficher la page pour voir tous vos annonces.
DashBoard/ViewCars. Permet d’afficher la page pour voir annonces de voitures du membre.
DashBoard/ViewBikes, permet d’afficher la page pour voir annonces de motos du membre.
DashBoard/ViewVans, permet d’afficher la page pour voir annonces de mini-fourgonnettes du membre.
Search/Car/City? Brand? Price? permet d’afficher la page de résultats de recherches des voitures.
Search/Bike/City? /Brand? /Price? permet d’afficher la page de résultats des motos.
Search/Van/City? /Brand? /Price? permet d’afficher la page de résultats des mini-fourgonnettes.
User/Index, permet d’afficher la liste des membres de la plateforme.
User/Edit, permet de modifier le profil des membres.
User/Delete, permet de supprimer le profil des membres.

Contrôleur:
1-	AccountController, permet aux utilisateurs de devenir membre et de se connecter.
2-	BikesController, permet aux membres de vendre et de voir leurs motos.
3-	CarsController, permet aux membres de vendre et de voir leurs voitures.
4-	VansController, permet aux membres de vendre et de voir leurs mini-fourgonnettes.
5-	HomeController, permet d’afficher la page d’accueil et les voitures à vendre à proximité.
6-	DashBoardController, permet de modifier ou supprimer le profil et les annonces du membre connecté.
7-	UserController, permet à l’administrateur de voir la liste des membres de la plateforme.
8-	SearchController, permets à tous de faire des recherches sur la plateforme.




Interface/Référentiel (Repository):
BikeRepository/IBikeRepository, add (), Update (), Delete (), Save (), GetById (), GetByCity ().
CarRepository/ICarRepository, add (), Update (), Delete (), Save (), GetById (), GetByCity ().
VanRepository/IVanRepository, add (), Save (), GetById (), GetByCity ().
DashBoardRepository/IDashBoardRepository, GetUserCars (), GetUserBikes (), GetUserVans (), GetUserId (), Update (), Save ().
PhotoService/IPhotoService, AddPhoto (), DeletePhoto (). (Cloud Service)
SearchRepository/ISearchRepository, GetCarsByCity (), GetCarsByPrice (), GetCarsByBrand (), GetCarsByModel (), GetCarsByYear (), GetBikesByCity (), GetBikesByPrice (), GetBikesByBrand (), GetBikesByModel (), GetBikesByYear (). GetVansByCity (), GetVansByPrice (), GetVansByBrand (), GetVansByModel (), GetVansByYear ().
UserRepository/IUserRepository, GetAllUsers (), GetUserById (), Delete (), Save (), Update ().


