# SNES Reference

## External Links

* **[SNESdev Wiki](https://snes.nesdev.org/wiki/SNESdev_Wiki)**
* **[Memory Map](https://snes.nesdev.org/wiki/Memory_map)** *(SNESdev Wiki)*
* **[65C816](https://snes.nesdev.org/wiki/65C816)** *(SNESdev Wiki)*
* **[65C816 Opcodes](http://www.6502.org/tutorials/65c816opcodes.html)** *by Bruce Clark*
* **[Obelisk 6502 Guide](https://www.nesdev.org/obelisk-6502-guide/)** *(Nesdev Wiki)*
* **[Super NES Programming](https://en.wikibooks.org/wiki/Super_NES_Programming)** *(Wikibooks)*
* **[WDC 65C816](https://en.wikipedia.org/wiki/WDC_65C816)** *(Wikipedia)*

## Internal Header
Location:  
- **LoROM**: `$7FC0–7FFF`  
- **HiROM**: `$FFC0–FFFF`

| Offset | Field               | Common Values |
|--------|---------------------|---------------|
| $00–$14 | Title              | ASCII string |
| $15     | Map Mode           | `$20`=LoROM, `$21`=Lo+Fast, `$30`=HiROM, `$31`=Hi+Fast, `$25/$35` extended 8 MiB |
| $16     | Cartridge Type     | `$00`=ROM, `$01`=ROM+RAM, `$02`=ROM+RAM+Batt, `$03`=ROM+DSP, later values for special chips |
| $17     | ROM Size           | `$08`=2 Mbit (256 KiB)… `$0C`=32 Mbit (4 MiB) |
| $18     | SRAM Size          | `$00`=none, `$01`=2 KiB, `$02`=8 KiB, `$03`=32 KiB |
| $19     | Destination Code   | `$00`=Japan, `$01`=USA, `$02`=Europe |
| $1A     | Licensee           | `$33`=extended code, `$01`=Nintendo (old system) |
| $1B     | Version            | `$00`=v1.0, `$01`=v1.1 |
| $1C–$1D | Complement checksum | Checksum ^ $FFFF |
| $1E–$1F | Checksum           | 16‑bit sum of ROM |

## Vectors (last bytes of header bank)

**LoROM**: `$7FE0–7FFF`  
**HiROM**: `$FFE0–$FFFF`

| Vector | Purpose            | Notes |
|--------|-------------------|-------|
| RESET  | Boot entry point   | Where CPU begins execution |
| NMI    | V‑Blank interrupt  | Runs once/frame during V‑Blank |
| IRQ    | IRQ/Timer interrupt| Raster effects, HDMA tricks |
| COP    | Co‑Processor trap  | Rare, sometimes used for debugging |
| BRK    | Software break     | Debug (mostly unused in games) |
| ABORT  | Abort trap         | Rare |
| Separate EMU vs NATIVE mode entries but **RESET, NMI, IRQ** are the critical 3 |

## CPU
- **[Ricoh 5A22](https://en.wikipedia.org/wiki/Ricoh_5A22)** (65C816‑based, backward compatible w/6502).
- **Registers**: 8/16‑bit modes for A, X, Y.  
- **Addressing**: 24‑bit = 16 MiB visible space.  
- **Effective speed**: ~3.58 MHz (varies by memory region).  

## Address Space Overview
- 16 MiB total, but mostly **mirrors**.  
- **Unique areas**:
  - **WRAM (work RAM)**: `$7E0000–7FFFFF` (128 KiB).  
  - **Cartridge ROM/RAM**: LoROM/HiROM mapping (up to 4 MiB standard, 8 MiB extended).
  - **I/O registers**: `$2100–$21FF` (PPU, DMA, etc.), `$4016/4017` (controllers), others at `$4200–437F`.  

Key point: Only ~4–8 MiB ROM + 128 KiB WRAM are non‑mirrored. Rest repeats.

### General Structure
- CPU address space: **24‑bit = 16 MiB (banks $00–FF, each 64 KiB).**
- **WRAM:** `$7E0000–7FFFFF` (128 KiB).  
- **Cartridge ROM/RAM:** mapped in **LoROM** or **HiROM** fashion.  
- **I/O & PPU registers:** `$2100–$21FF`, `$4016–4017`, `$4200–437F`.  
- **Mirrors:** huge parts of space just repeat.

## Memory Mappers

### Overview LoROM vs HiROM

| Feature       | LoROM                             | HiROM                          |
|---------------|-----------------------------------|--------------------------------|
| Bank size     | 32 KiB mapped to `$8000–FFFF`     | 64 KiB mapped to `$0000–FFFF` |
| Max ROM       | 4 MiB                             | 4 MiB                         |
| Extended mode | ExLoROM → 8 MiB                   | ExHiROM → 8 MiB               |
| Typical use   | Early games, action/platformers   | RPGs, large later games        |

**ExLo/ExHi**: Use normally unused banks → up to 8 MiB. Header values: `$25` (ExLo), `$35` (ExHi).

### LoROM Layout (standard 4 MiB)

```
Bank $00–$3F, $80–$BF

$0000–1FFF  → WRAM mirrors
$2000–20FF  → PPU/APU I/O regs
$2100–21FF  → More registers
$4000–41FF  → Controller ports, misc I/O
$4200–437F  → DMA, timers, hardware control
$4380–7FFF  → Typically open/unused or mirrors
$8000–FFFF  → ROM (32 KiB per bank)

Banks $00–3F:$8000–FFFF
   and $80–BF:$8000–FFFF
   → First 4 MiB of ROM (LoROM mapping)

Banks $7E–7F  → 128 KiB Work RAM
```

**LoROM ExLoROM (8 MiB):**  
Banks `$40–7D:$8000–FFFF` and `$C0–FF:$8000–FFFF` map second 4 MiB.

### HiROM Layout (standard 4 MiB)

```
Bank $00–$3F, $80–$BF

$0000–1FFF  → WRAM mirrors
$2000–20FF  → PPU/APU I/O
$2100–21FF  → More registers
$4000–41FF  → Controllers / misc I/O
$4200–437F  → DMA, timers
$4380–7FFF  → Typically open/unused
$8000–FFFF  → ROM (partial coverage here)

Bank $40–$7D, $C0–$FF

$0000–FFFF  → ROM (64 KiB per bank)

Banks $40–7D  → First 4 MiB of ROM
Banks $C0–FF  → Mirror

Banks $7E–7F  → 128 KiB Work RAM
```

**HiROM ExHiROM (8 MiB):**  
Additional ROM mapped into normally unused spaces. Header byte: `$35`.

### Diagrammatic Summary

```
SNES CPU Address Space (24-bit)   Size      LoROM                  HiROM
-------------------------------   -------   ---------------------  -------------------
$00–$3F:0000–1FFF                 8 KiB     WRAM mirrors           WRAM mirrors
$00–$3F:2000–21FF                 512 B     PPU/APU regs           PPU/APU regs
$00–$3F:4200–437F                 384 B     DMA/timers/etc         DMA/timers/etc
$00–$3F:8000–FFFF                 32 KiB    ROM banks              Mostly open / partial ROM

$40–$7D:0000–FFFF                 64 KiB    Open/mirrors (ExLoROM) ROM banks (HiROM main area)
$7E–$7F:0000–FFFF                 128 KiB   WRAM                   WRAM
$80–$BF:8000–FFFF                 32 KiB    Mirror of $00–3F       Mirror of LoROM low space
$C0–$FF:0000–FFFF                 64 KiB    Open/mirrors (ExLoROM) Mirror of $40–7D
```

### Errata
- **LoROM** → $8000–FFFF area in each bank (32 KiB chunks). Convenience for small programs, common in action/platformers.  
- **HiROM** → $0000–FFFF area in each bank (64 KiB chunks). Better for data‑heavy RPGs.  
- **WRAM always in $7E–7F** (128 KiB contiguous).  
- **I/O regs always in $2100‑21FF & $4200+** regardless of mapping.  
- Extended mappings (**ExLo/ExHi**) double limit to 8 MiB.  

## SlowROM vs FastROM
- **SlowROM:** 200 ns cycles (~2.68 MHz effective to ROM).  
- **FastROM:** 120 ns cycles (~3.58 MHz). Faster mask ROM chips needed.  
- Controlled by map mode byte in header:
  - `$20` = LoROM + Slow  
  - `$21` = LoROM + Fast  
  - `$30` = HiROM + Slow  
  - `$31` = HiROM + Fast  
