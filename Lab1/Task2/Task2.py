import random
import math

R = 5


def is_on_halfcircle_border(x, y, R):
    eps = 1e-9
    dist2 = x * x + y * y
    r2 = R * R
    on_diameter = abs(y) < eps and -R <= x <= R
    on_arc = abs(dist2 - r2) < eps and y >= 0
    return on_diameter or on_arc


def is_inside_halfcircle(x, y, R):
    dist2 = x * x + y * y
    r2 = R * R
    return y > 0 and dist2 < r2


def is_on_triangle_border(x, y, R):
    eps = 1e-9
    on_ab = abs(x) < eps and -R <= y <= 0
    on_bc = abs(y + R) < eps and -R <= x <= 0
    on_ac = abs(y - x) < eps and -R <= x <= 0 and -R <= y <= 0
    return on_ab or on_bc or on_ac


def is_inside_triangle(x, y, R):
    if x < -R or x > 0:
        return False
    if y <= -R or y >= x:
        return False
    return True


def check_point(x, y, R):
    if is_on_halfcircle_border(x, y, R) or is_on_triangle_border(x, y, R):
        return -1
    if is_inside_halfcircle(x, y, R) or is_inside_triangle(x, y, R):
        return 1
    return 0


def generate_shot():
    return random.randint(-R, R), random.randint(-R, R)


print(f"{'Shot #':<10}{'Coordinates':<20}{'Result'}")
for i in range(1, 11):
    x, y = generate_shot()
    res = check_point(x, y, R)
    if res == 1:
        result = "hit the target"
    elif res == -1:
        result = "on the border (hit)"
    else:
        result = "missed"
    print(f"{i:<10}({x}, {y}){'':<8}{result}")
