import {useStore} from "../../../global/stores/store";
import {Box, Button} from "@mui/material";
import CreateProductModal from "../../onlineShop/productCreation/CreateProductModal";
import {AddCircleOutlined} from "@mui/icons-material";
import React from "react";
import {observer} from "mobx-react-lite";
import PriceListList from "../priceListList/PriceListList";
import PriceListCreateModal from "./PriceListCreateModal";

function PriceListCreateSection() {
    const {modalStore} = useStore()

    return (
        <Box display={'flex'} justifyContent={'right'}>
            <Button color='secondary' style={{display: 'flex', gap: '10px', backgroundColor: '#C32B28',
                justifyContent: 'center', alignItems: 'center'}} onClick={() => modalStore.openModal(<PriceListCreateModal />)}>
                <AddCircleOutlined fontSize='medium' />
                Dodaj nowy cennik
            </Button>
        </Box>
    )
}

export default observer(PriceListCreateSection)