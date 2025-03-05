const prompt = require('prompt-sync')();
const puppeteer = require('puppeteer');

let option;

const url = 'https://docs.jitterbit.com';

do {
    console.log("--------------------");
    console.log("Web Scraping JB Docs");
    console.log("--------------------");
    console.log("Escolha as opções");
    console.log("1. Salvar documentação de uma página em um arquivo");
    console.log("2. Sair");
    console.log("");

    option = prompt('Digite: ');

    switch (option) {
        case "1":
            console.log('Escolha a página que deseja visualizar');
            console.log("1. Getting started");
            console.log("2. Release notes");
            console.log("3. iPaaS");
            console.log("4. API Manager");
            console.log("5. App Builder");
            console.log("6. EDI");
            console.log("7. Marketplace");
            console.log("8. Management Console");
            const option_page = prompt('Digite: ');

            let path;
            switch (option_page) {
                case "1":
                    path = "/getting-started/";
                    break;
                case "2":
                    path = "/release-notes/";
                    break;
                case "3":
                    path = "";
                    break;
                case "4":
                    path = "/api-manager/";
                    break;
                case "5":
                    path = "/app-builder/";
                    break;
                case "6":
                    path = "/edi/";
                    break;
                case "7":
                    path = "/marketplace/";
                    break;
                case "8":
                    path = "/management-console/";
                    break;
                default:
                    console.log("Opção inválida!");
                    continue;
            }

            console.log('Escolha o nome do arquivo: ');
            const file_name = prompt("Digite o nome: ");
            console.log(`Buscando links da página ${url + path}...`);

            (async () => {
                const browser = await puppeteer.launch({ headless: false });
                const page = await browser.newPage();
                await page.goto(url + path);
            
                
                await page.evaluate((path, file_name) => {
                    let nomearquivo = file_name;

                    let links = document.getElementsByClassName('md-nav__link');
                    let linksArray = Array.prototype.slice.call(links);

                    links = linksArray.map((Element) => {
                        return "https://docs.jitterbit.com" + path + Element.getAttribute("href");
                    });

                    const ArrayNoNull = links.filter(x => x != 'https://docs.jitterbit.com' + path + 'null');
                    const content = ArrayNoNull.join("\n");

                    const blob = new Blob([content], { type: "text/plain" });
                    const a = document.createElement("a");
                    const url = URL.createObjectURL(blob);

                    a.href = url;
                    a.download = nomearquivo;

                    document.body.appendChild(a);
                    a.click();

                    URL.revokeObjectURL(url);
                }, path, file_name);

                await browser.close(); 
            })();
            break;

        case "2":
            console.log("Saindo do programa...");
            break;

        default:
            console.log("Opção inválida!");
    }
} while (option !== "2");
