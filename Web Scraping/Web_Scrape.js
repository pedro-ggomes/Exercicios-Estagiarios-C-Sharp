// Importa a biblioteca puppeteer
const puppeteer = require('puppeteer');

// chama e executa uma func async
(async () => {
    // Inicia o browser visível ao usuário
    const browser = await puppeteer.launch({ headless: false }); 

    // Abre uma nova guia no navegador
    const page = await browser.newPage();

    // Abre a guia correta que iremos extrair os dados
    await page.goto('https://docs.jitterbit.com/');

    // Executa uma sequência de comandos no navegador
    await page.evaluate(() => {
        // Nome do arquivo final
        let nomearquivo = "links_Pedrão.txt"

        // Armazena todos os elementos com classe nome na variável links
        let links = document.getElementsByClassName('md-nav__link')

        // Seleciona todos os elementos da htmlCollection links e a transforma em array
        let linksArray = Array.prototype.slice.call(links)

        // Acessa todos os elementos de linksArray e retorna o seu href, que é o caminho da página
        links = linksArray.map((Element) => { return "https://docs.jitterbit.com/app-builder/" + Element.getAttribute("href") })

        // Remove do array os itens 'https://docs.jitterbit.com/app-builder/null'
        const ArrayNoNull = links.filter(x => x != 'https://docs.jitterbit.com/app-builder/null')

        // Converte o array para uma string, separando por quebras de linha (\n)
        const content = ArrayNoNull.join("\n");

        // Cria um objeto blob com a string content
        const blob = new Blob([content], { type: "text/plain" });

        // Cria um elemento <a> para download
        const a = document.createElement("a");

        // Cria um URL temporário para o Blob
        const url = URL.createObjectURL(blob);

        // Seleciona o url temporário criado e da o nome ao arquivo
        a.href = url;
        a.download = nomearquivo;

        // Adiciona ao DOM, clica nele para iniciar o download e o remove
        document.body.appendChild(a);
        a.click();

        // Revoga o URL para liberar memória
        URL.revokeObjectURL(url);
    });
})();
