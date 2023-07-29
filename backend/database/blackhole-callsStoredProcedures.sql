USE blackhole;

CALL registerAchievement(@id, "Primeros pasos", "Obtuviste tu primera respuesta correcta");
CALL registerAchievement(@id, "Culpa al manual", "Fallaste por primera vez");
CALL registerAchievement(@id, "El problema ya no es el manual...", "Fallaste tres veces");
CALL registerAchievement(@id, "Consigue otro compañero, por favor", "No obtuviste ninguna respuesta correcta");
CALL registerAchievement(@id, "No deberías tener este logro", "Fallaste más de 10 veces");
CALL registerAchievement(@id, "Aún no hay BlackHole 2", "Ganaste sin fallar");
CALL registerAchievement(@id, "¿Hacks para un juego educativo?", "Ganaste sin fallar en menos de 12 minutos");
CALL registerAchievement(@id, "Mejor revisa tus pantalones", "Ganaste por un tiempo restante menor a 10 segundos");
CALL registerAchievement(@id, "TOC", "Ganaste con un error");
CALL registerAchievement(@id, "No es adivinanza", "Ganaste con más de 5 errores");
CALL registerAchievement(@id, "No, no se puede disminuir la dificultad", "Perdiste sin ninguna respuesta correcta");
CALL registerAchievement(@id, "Sí, todo de nuevo", "Perdiste con 3 respuestas correctas");
