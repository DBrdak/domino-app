import { Add, AddCircleOutlined, AddOutlined } from '@mui/icons-material'
import {Box, Button, IconButton, Stack, TextField} from '@mui/material'
import React, {useEffect, useMemo, useState} from 'react'
import { useStore } from '../../../global/stores/store'
import CreateProductModal from './CreateProductModal'
import { observer } from 'mobx-react-lite'

interface Props {
    onApplySearch: (productName: string | null) => void;
}

function ProductUpperSection({onApplySearch}: Props) {
    const {adminPriceListStore, modalStore, adminProductStore} = useStore()
    const [names, setNames] = useState<string[]>([])
    const [searchPhrase, setSearchPhrase] = useState<string | null>(null)
    const [debouncedSearchPhrase, setDebouncedSearchPhrase] = useState<string | null>(null);

    useEffect(() => {
        const timerId = setTimeout(() => {
            setDebouncedSearchPhrase(searchPhrase);
        }, 400);

        return () => {
            clearTimeout(timerId);
        };
    }, [searchPhrase]);

    useEffect(() => {
        onApplySearch(debouncedSearchPhrase);
    }, [debouncedSearchPhrase]);


    const handleApplySearch = (productName: string | null) => {
        setSearchPhrase(productName)
    }

    useEffect(() => {
        const getNames = async () => {
            await adminPriceListStore.loadPriceLists()
            const retailPriceList = [...adminPriceListStore.priceLists].filter(pl => pl.contractor.name === 'Retail')
            const nonAggregatedProductNames = retailPriceList &&
                retailPriceList.flatMap(pl => pl.lineItems).filter(li => li.productId == null).map(li => li.name)
            nonAggregatedProductNames && setNames(nonAggregatedProductNames)
        }
        getNames()
    }, [adminPriceListStore, setNames, adminProductStore.products])

      return (
          <Stack direction={'row'} display={'flex'} justifyContent={'space-between'}>
              <TextField
                  style={{width: '30%'}}
                  label="Nazwa produktu"
                  onChange={(e) => handleApplySearch(e.target.value)}
              />
              <Button color='primary' variant={'contained'} disabled={names[0] ? false : true} style={{
                  display: 'flex', gap: '10px', justifyContent: 'center', alignItems: 'center'}}
                  onClick={() => modalStore.openModal(<CreateProductModal setNames={setNames} names={names}/>)}>
                  <AddCircleOutlined fontSize='medium'/>
                  Dodaj nowy produkt
              </Button>
          </Stack>
      )
}

export default observer(ProductUpperSection)