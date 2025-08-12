import sys, json, os, re

ISSUE_TEXT = sys.stdin.read()

# Path inside the repo
BALANCE_PATH = os.path.join("Assets", "Resources", "Balance.json")

# Accept lines like:
# PLAYER_SPEED = 7.5
# JUMP_FORCE: 6.8
# MAX_LIVES=5
pattern = re.compile(r'^\s*([A-Z_]+)\s*[:=]\s*([-+]?\d+(\.\d+)?)\s*$', re.MULTILINE)

updates = {}
for m in pattern.finditer(ISSUE_TEXT):
    key = m.group(1).strip().upper()
    val = float(m.group(2))
    # int keys
    if key in ("MAX_LIVES", "COINS_PER_PICKUP"):
        val = int(round(val))
    updates[key] = val

if not os.path.exists(BALANCE_PATH):
    # create default if missing
    data = {
        "PLAYER_SPEED": 5.0,
        "JUMP_FORCE": 6.5,
        "GRAVITY": -9.81,
        "MAX_LIVES": 3,
        "COINS_PER_PICKUP": 1
    }
else:
    with open(BALANCE_PATH, "r", encoding="utf-8") as f:
        try:
            data = json.load(f)
        except Exception:
            data = {}

changed = []
for k, v in updates.items():
    old = data.get(k)
    if old != v:
        data[k] = v
        changed.append((k, old, v))

# Write back
os.makedirs(os.path.dirname(BALANCE_PATH), exist_ok=True)
with open(BALANCE_PATH, "w", encoding="utf-8") as f:
    json.dump(data, f, indent=2, ensure_ascii=False)

print("Updated keys:", ", ".join(k for k,_,_ in changed) if changed else "none")
