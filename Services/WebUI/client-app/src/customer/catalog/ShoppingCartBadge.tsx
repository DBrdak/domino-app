import { ShoppingCart } from '@mui/icons-material'
import { Fab, Badge } from '@mui/material'
import React from 'react'
import { ShoppingCartItem } from '../../global/models/shoppingCart'
import { observer } from 'mobx-react-lite'
//Tu trzeba store jebnąć
interface Props {
  shoppingCartItems: ShoppingCartItem[]
}

function ShoppingCartBadge({shoppingCartItems}:Props) {
  return (
    <div style={{ position: 'fixed', right: 20, bottom: 20 }}>
      <Fab color="primary" aria-label="add to shopping cart">
        <Badge badgeContent={shoppingCartItems.length} color="secondary">
          <ShoppingCart />
        </Badge>
      </Fab>
    </div>
  )
}

export default observer(ShoppingCartBadge)