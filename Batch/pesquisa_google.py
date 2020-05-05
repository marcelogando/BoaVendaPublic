from googleapiclient.discovery import build
from pymongo import MongoClient
from datetime import datetime
import json
#requests da lib no lambda - para rodar localmente -> import requests
# import requests
from botocore.vendored import requests

def lambda_handler(event, context):
    my_api_key = "##############################"
    my_cse_id = "#############################"

    client = MongoClient('########################')
    db = client.boavenda

    collection_busca_site_cliente = db['BuscaSiteCliente']
    collection_resultado_busca_google = db['ResultadoBuscaGoogle']

    def google_search(search_term, api_key, cse_id, start):
        r = requests.get('https://customsearch.googleapis.com/customsearch/v1?q=' + search_term + '&cx=' + cse_id + '&key=' + api_key + '&start=' + str(start) + '&gl=br&lr=lang_pt')
        if 'items' in r.json():
            return r.json()['items']
        else:
            return []

    #buscar no mongo e loopar
    cursor = collection_busca_site_cliente.find()
    buscas = list(cursor)

    for b in buscas:
        link_busca = b['link']
        busca = b['busca']

        results = []
        for i in range(0,5):
            results.extend(google_search(busca, my_api_key, my_cse_id, i*10+1))

        for i, result in enumerate(results):
            if link_busca in result['link']:
                dados = {}
                
                dados['posicaoGoogle'] = i+1

                try:
                    dados['buscaId'] = b['buscaId']
                except:
                    dados['buscaId'] = b['_id']

                try:
                    dados['preco'] = float(result['pagemap']['offer'][0]['price'])
                except:
                    dados['preco'] = None

                try:
                    dados['imagem'] = result['pagemap']['cse_image'][0]['src']
                except:
                    dados['imagem'] = None

                try:
                    dados['review'] = result['pagemap']['review']
                except:
                    dados['review'] = None

                dados['dtIns'] = datetime.utcnow()
                collection_resultado_busca_google.insert_one(dados)

    return {
        'statusCode': 200,
        'body': json.dumps('Rodou')
    }

# lambda_handler(None,None)