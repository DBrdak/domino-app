import { Add, AddCircleOutlined, AddOutlined } from '@mui/icons-material'
import {Box, Button, IconButton, Stack, TextField} from '@mui/material'
import React, {useEffect, useMemo, useState} from 'react'
import { useStore } from '../../../global/stores/store'
import { observer } from 'mobx-react-lite'
import ShopCreateModal from "./modals/shop/ShopCreateModal";
import {ShopCreateValues} from "../../../global/models/shop";

function ShopsListUpperSection() {
    const {modalStore} = useStore()

    return (
        <Box display={'flex'} justifyContent={'right'}>
            <Button color='primary' variant={'contained'} style={{
                display: 'flex', gap: '10px', justifyContent: 'center', alignItems: 'center'}}
                    onClick={() => modalStore.openModal(<ShopCreateModal />)}>
                <AddCircleOutlined fontSize='medium'/>
                Dodaj nowy sklep
            </Button>
        </Box>
    )
}

export default observer(ShopsListUpperSection)