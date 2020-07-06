# ProgRedesParcial
Parcial de Programación de Redes Julio 2020

## Admin Web Server

El diseño del admin web server no contiene necesidad de autenticación.
|ENDPOINT                               |GET                                            |POST                                                    |PUT                                            |DELETE                                                           |
|---------------------------------------|-----------------------------------------------|--------------------------------------------------------|-----------------------------------------------|-----------------------------------------------------------------|
|Sessions                               |-                                              |Crea una sesion, asi logueando a un usuario.            |-                                              |-                                                                |
|Sessions/{sessionId}                   |Obtiene la session relevante.                  |-                                                       |-                                              |Elimina una sesión, así deslogueado a un usuario.                |
|Films                                  |Obtiene el listado de películas.               |Crea una película.                                      |-                                              |-                                                                |
|Films/{filmId}                         |Obtiene una película con el id correspondiente.|-                                                       |Modifica la pelicula con el id correspondiente.|Elimina la película con el id correspondiente.                   |
|Films/{filmId}/ratings                 |-                                              |Crea un rating para la película con filmId.             |-                                              |-                                                                |
|Films/{filmId}/ratings/{ratingId}      |-                                              |-                                                       |Modifica un rating para la película con filmId.|Elimina un rating para la película con filmId.                   |
|Films/{filmId}/genres                  |-                                              |Agrega un género para la película con filmId.           |-                                              |-                                                                |
|Films/{filmId}/genres/{genreId}        |-                                              |-                                                       |-                                              |Elimina un género para la película con filmId.                   |
|Films/{filmId}/files                   |-                                              |Agrega un archivo para la película con filmId.          |-                                              |-                                                                |
|Directors                              |Obtiene el listado de directores.              |Agrega un director.                                     |-                                              |-                                                                |
|Directors/{directorId}                 |Obtiene el director con directorID.            |-                                                       |Modifica el director con directorID.           |Elimina el director con directorID.                              |
|Genres                                 |Obtiene el listado de géneros.                 |Agrega un género.                                       |-                                              |-                                                                |
|Genres/{genreId}                       |Obtiene el género con genreId.                 |-                                                       |Modifica el género con genreId                 |Elimina el género con genreId                                    |
|Users                                  |Obtiene el listado de usuarios.                |Agrega un usuario.                                      |-                                              |-                                                                |
|Users/{userId}                         |Obtiene el usuario con userId.                 |-                                                       |Modifica el usuario con userId.                |Elimina el usuario con userId.                                   |
|Users/{userId}/favourites              |-                                              |Agrega una película favorita para el usuario con userId.|-                                              |-                                                                |
|Users/{userId}/favourites/{favouriteId}|-                                              |-                                                       |-                                              |Elimina la película con favouriteId como favorita para el userId.|

## User Web Server

El diseño de la web api utiliza HttpClient para así funcionar como proxy del Admin Web Server. Los siguientes endpoints son los identificados:

|ENDPOINT                         |GET                             |POST                                           |PUT                                            |DELETE                                            |
|---------------------------------|--------------------------------|-----------------------------------------------|-----------------------------------------------|--------------------------------------------------|
|Sessions                         |-                               |Crea una sesion, asi logueando a un usuario.   |-                                              |-                                                 |
|Sessions/{sessionId}             |-                               |-                                              |-                                              |Elimina una sesión, así deslogueado a un usuario. |
|Films                            |Obtiene el listado de películas.|-                                              |-                                              |-                                                 |
|Films/{filmId}                   |Obtiene una película con filmId.|-                                              |Modifica la pelicula con filmId.               |Elimina la película con filmId.                   |
|Films/{filmId}/ratings           |-                               |Crea un rating de un usuario para una película.|-                                              |-                                                 |
|Films/{filmId}/ratings/{ratingId}|-                               |-                                              |Crea un rating de un usuario para una película.|Elimina un rating de un usuario para una película.|
|Favourites                       |-                               |Crea un favorito para un usuario.              |-                                              |-                                                 |
|Favourites/{id}/favourites/{id}  |-                               |-                                              |-                                              |Elimina un favorito para un usuario.              |
