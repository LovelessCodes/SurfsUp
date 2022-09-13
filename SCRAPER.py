# SCRAPER
import requests
from bs4 import BeautifulSoup

urls = [
    "https://www.kiteworldshop.com/en/48-allaround-sup-boards",
    "https://www.kiteworldshop.com/en/45-solid-sup",
    "https://www.kiteworldshop.com/en/52-whitewater-sup-boards",
    "https://www.kiteworldshop.com/en/67-surf",
    "https://www.kiteworldshop.com/en/137-windsurf-boards",
    "https://www.kiteworldshop.com/en/50-race-sup-boards"
]
boards = []

for ul in urls:
    r = requests.get(ul)
    soup = BeautifulSoup(r.text, features='html.parser')
    boards.extend([k.select_one("a").attrs.get("href") for k in soup.select("li.col-ms-4")])

for board in list(set(boards)):
    try:
        r = requests.get(board)
        soup = BeautifulSoup(r.text, features='html.parser')
        print("Title: "+soup.select_one(".page-heading").text)
        print("Volume: "+soup.select_one("tr.odd:nth-child(7) > td:nth-child(2)").text)
        print("Length (cm): "+soup.select_one("tr.even:nth-child(2) > td:nth-child(2)").text)
        print("Width (cm): "+soup.select_one("tr.odd:nth-child(3) > td:nth-child(2)").text.replace(" cm", "").rstrip().lstrip())
        print("Thickness (cm): "+soup.select_one("tr.even:nth-child(4) > td:nth-child(2)").text.replace(" cm", "").rstrip().lstrip())
        print("Image: "+soup.select_one("#bigpic").attrs.get("src"))
        print("Type: "+soup.select_one("span.navigation_page:nth-child(7) > a:nth-child(1) > span:nth-child(1)").text)
        equipment = soup.select_one("tr.odd:nth-child(5) > td:nth-child(2)").text.replace('"', "'")
        with open("surfboards.txt", "a+") as f:
            f.write(
                "new Surfboard\n{\n    Title = \""+
                f"{soup.select_one('.page-heading').text}\",\n    Thickness = "+
                f'{soup.select_one("tr.even:nth-child(4) > td:nth-child(2)").text.split("/")[1].replace(" cm", "").rstrip().lstrip()}F,\n    Width = '+
                f'{soup.select_one("tr.odd:nth-child(3) > td:nth-child(2)").text.split("/")[1].replace(" cm", "").rstrip().lstrip()},\n    Length = '+
                f'{soup.select_one("tr.even:nth-child(2) > td:nth-child(2)").text},\n    Volume = '+
                f'{soup.select_one("tr.odd:nth-child(7) > td:nth-child(2)").text}F,\n    Type = "'+
                f'{soup.select_one("span.navigation_page:nth-child(7) > a:nth-child(1) > span:nth-child(1)").text}",\n    Price = '+
                f'{soup.select_one("#our_price_display").text.replace(" €", "").replace(",", ".")},\n    Equipment = "'+
                f'{equipment}"\n'+
                '},\n'
            )
    except Exception as e:
        print(e)