import random
def step1(shu, color, pays):
    if color == "Rouge":
        shu += 3
        return step2(shu, color, pays)
    elif color == "Vert":
        shu -= 5.5
        return step3(shu, color, pays)
    else:
        shu += 6.1
        return step4(shu, color, pays)


def step2(shu, color, pays):
    if pays.startswith("I"):
        shu += 7.6
        return step5(shu, color, pays)
    else:
        shu -= 7.9
        return step8(shu, color, pays)


def step3(shu, color, pays):
    if pays in ["Inde", "Canada"]:
        shu += 0.7
        return step6(shu, color, pays)
    elif pays == "Chine":
        shu -= 2
        return step4(shu, color, pays)
    elif pays == "Indonésie" and shu > 9:
        return 2
    elif pays == "Brésil":
        shu *= 2
        return step5(shu, color, pays)
    else:
        shu *= 3
        return step4(shu, color, pays)


def step4(shu, color, pays):
    # Étape 16 : on répète l'étape précédente
    if False:
        return step3(shu, color, pays)

    if shu > 5:
        return step9(shu, color, pays)
    elif False:  # Le piment a l'air joli
        shu += 9
        return step10(shu, color, pays)
    else:
        return step6(shu, color, pays)


def step5(shu, color, pays):
    if pays == "Indonésie":
        return shu + random.choice([3, -3])
    elif color != "Rouge":
        shu += 15
        return niveau_final(shu)
    else:
        shu += 351
        return niveau_final(shu)


def step6(shu, color, pays):
    if 5 <= shu <= 6:
        return step1(shu, color, pays)
    elif pays == "Canada":
        print("Ton piment est magnifique !")
        shu /= 2
        return niveau_final(shu)
    else:
        return step9(shu, color, pays)


def step7(shu, color, pays, step=6):
    # Répète l'étape précédente : étape 6
    if step==6:
        return step6(shu, color, pays)
    else:
        return step8(shu, color, pays)


def step8(shu, color, pays):
    shu /= 2
    if -2 < shu < 5:
        return step9(shu, color, pays)
    else:
        return step7(shu, color, pays, 8)


def step9(shu, color, pays):
    amerique_nord = ["Canada", "États-Unis", "Mexique"]

    if pays in amerique_nord:
        shu += 4.4
    else:
        shu -= 4.4

    return niveau_final(shu)


def step10(shu, color, pays):
    shu -= 9
    return step9(shu, color, pays)


def step16(shu, color, pays):
    # Répète l'étape précédente
    return step4(shu, color, pays)


def niveau_final(shu):
    while shu < 0 or shu > 10:
        if shu < 0:
            shu += 10
        elif shu > 10:
            shu -= 10
    return shu


# Exemple d'utilisation :
print(step1(0, "Rouge", "Brésil"))
