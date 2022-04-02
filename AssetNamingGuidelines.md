# Asset Naming Guidelines
- In dieser Datei sind Regeln zum Bennennen von Assets/ Dateien aufgelistet

## Allgemeine Regeln
- PascalCase für Dateinamen verwenden
- Nie Leerzeichen benutzen
- Keine Umlaute oder Sonderzeichen verwenden außer in Ausnahmefällen (z.B. @game)
- An editor-spezifische Benennung/ Funktionalität denken, z.B. `Editor` Ordner
- Objekte in der Unity-Szenenhierarchie sinnvoll benennen, also statt Object1, Object2 lieber Pfosten1, Lampe1

## Suffix und Prefix von Datei-Typen
| Asset Type                            | Prefix | Suffix | Notes                                                   |
|---------------------------------------|--------|--------|---------------------------------------------------------|
| Scene/ Level                          |        |        | Should be in a folder called `Scenes`                   |
| Material                              | M_     |        |                                                         |
| Static Mesh                           | SM_    |        |                                                         |
| Skelatal Mesh                         | SK     |        |                                                         |
| Particle System                       | PS_    |        |                                                         |
| Script                                |        |        | No prefix/ suffix because of Unity editor functionality |
| Texture                               | T_     |        | See more detailted types below                          |
| Texture (Diffuse/ Albedo/ Base Color) | T_     | _D     |                                                         |
| Texture (Normal)                      | T_     | _N     |                                                         |
| Texture (Roughness)                   | T_     | _R     |                                                         |
| Texture (Alpha/ Opacity)              | T_     | _A     |                                                         |
| Texture (Ambient Occlusion)           | T_     | _AO    |                                                         |
| Texture (Height)                      | T_     | _H     |                                                         |
| Texture (Emissive)                    | T_     | _E     |                                                         |
| Texture (Metallic)                    | T_     | _M     |                                                         |
| Texture (Mask)                        | T_     | _MK    |                                                         |
| Texture (Sky/ Environment)            | T_     | _ENV   |                                                         |
| Texture (Packed)                      | T_     | _*     | Suffix combination of packed maps, e.g. _RHM            |
| Sound                                 | S_     |        |                                                         |
| Animation                             | A_     |        |                                                         |
| Animation (Sitting)                   | A_Si_  |        |                                                         |
| Animation (Standing)                  | A_St_  |        |                                                         |
| Font                                  | F_     |        |                                                         |
