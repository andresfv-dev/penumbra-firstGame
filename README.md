# Nombre del Proyecto

## 📌 Descripción

Breve explicación de qué trata el juego o sistema.

## 🛠️ Tech Stack

- **Unity Version:** 6.3.X LTS
- **Render Pipeline:** URP
- **Lenguaje:** C# (NET 8/9 compatible con Unity 6)

## 🛠️ Configuración del Editor

Para optimizar el flujo de trabajo, este proyecto utiliza:

1. **Manual Refresh:** Presiona `Ctrl + R` para compilar cambios de scripts.
2. **Fast Play Mode:** Configurado en `Project Settings > Editor ` Baja a Enter Play Mode Settings y desactiva ambos Reload Domain y Reload Scene para evitar el Reload
   Domain.

## 📂 Estructura de Carpetas

- `JuegoFalla`: Lógica principal y assets.
  Assets/
  ├── 01_Project/ # Todo el contenido específico del juego
  │ ├── Art/ # Arte compartido (Modelos, Materiales, Shaders)
  │ ├── Audio/ # SFX, Música, Mezcladores
  │ ├── Core/ # Sistemas base (GameManager, Singleton base, etc.)
  │ ├── Modules/ # El corazón de la escalabilidad (Estructura por función)
  │ │ ├── Player/  
  │ │ ├── Inventory/  
  │ │ └── Enemies/  
  │ ├── Prefabs/ # Prefabs globales o de UI
  │ ├── Settings/ # Config de Unity 6 (URP/HDRP Settings, Input Actions)
  │ └── UI/ # Fuentes, Sprites de interfaz, Menús

## 🚀 Configuración del Entorno

1. Clonar el repositorio.
2. Abrir con Unity Hub usando la versión 6.3.X.
3. El proyecto usa **Manual Refresh** (`Ctrl+R` para compilar).

## ⚠️ Reglas de Desarrollo

- Usar **Assembly Definitions** para cada nuevo módulo en `/Modules`.
- No subir la carpeta `Library` ni `Temp` (asegurado por .gitignore).
- Formato de nombres: PascalCase para scripts y prefijos `T_`, `M_`, `P_` para assets.
