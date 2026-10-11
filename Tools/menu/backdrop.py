"""The main menu's backdrop (4i-A; the Crossroads still, 2026-10-10): a 320x180 capture of Kariaston from the explicit
CaptureMenuBackdrops test (BatchLogs/menu/<name>.png, the keeper and the HUD out of frame), given a little warmth and a soft
vignette, written to Assets/_Project/Art/Menu/MenuBackdrop.png.

    python Tools/menu/backdrop.py BatchLogs/menu/tallyho_left.png
"""
import sys
import numpy as np
from PIL import Image

WARMTH = (1.04, 1.0, 0.86)       # a little gold over the daylight
VIGNETTE = 0.45                  # how much the corners darken
INNER, OUTER = 0.45, 1.0         # where the vignette starts and where it's full (0 centre, 1 corner)


def treat(path, out='Assets/_Project/Art/Menu/MenuBackdrop.png'):
    a = np.asarray(Image.open(path).convert('RGB')).astype(float)
    h, w, _ = a.shape
    yy, xx = np.mgrid[0:h, 0:w]
    r = np.sqrt(((xx + .5 - w / 2) / (w / 2)) ** 2 + ((yy + .5 - h / 2) / (h / 2)) ** 2) / np.sqrt(2)
    t = np.clip((r - INNER) / (OUTER - INNER), 0, 1)
    fade = 1 - VIGNETTE * t * t * (3 - 2 * t)
    a = a * np.array(WARMTH) * fade[..., None]
    Image.fromarray(np.clip(a, 0, 255).round().astype(np.uint8)).save(out)
    return out


if __name__ == '__main__':
    print(treat(sys.argv[1]))
